const $articlesResultElement = $('.articles-result');
var articleArray = null;

//handlers
$(window).on('load', function()
{
    setTimeout(function(){
        LoadArticleFromDb();
    },500);
});

$(".search-text-input").keyup(function(){
    var searchText = $(".search-text-input").val();

    $articlesResultElement.empty();
    LoadArticleView(searchText);
});

//functions
function LoadArticleView(filterValue)
{
    var iCounterAddedArticles = 0;

    if(articleArray != null 
        && articleArray.length > 0)
    {
        articleArray.forEach(function(article) 
        {
            if( IsCompatibilityWithFilter(article,filterValue) )
            {
                $articlesResultElement.append(
                    '<li class="list-group-item article-result">'
                            + 'Temat: '                 + article.subject
                            + '<br/>Treść: '            + article.content
                            + '<br/>Kategoria: '        + article.articleCategory.name
                            + '<br/>Data modyfikacji: ' + article.articleModificationDate
                            + '<button class="see-article-button btn-success" type="submit" value="' + article.id + '">Zobacz</button>'
                    +  '</li>'); 

                    iCounterAddedArticles++;
            }      
        });
    }

    if( iCounterAddedArticles <= 0 )
    {
        $articlesResultElement.append('<li class="list-group-item article-result">Brak artykułów spełniających kryteria wyszukiwania!</li>');
    }
}

function IsCompatibilityWithFilter(article, filterValue)
{
    var bIsCompability = false;
    var filterValueLowerCase = filterValue.toLowerCase();

    if(article != null)
    {
        
        bIsCompability = ( filterValue == "" 
                        || article.content.toLowerCase().includes(filterValueLowerCase) );
    }

    return bIsCompability;
}

function LoadArticleFromDb()
{
    articleArray = new Array();

    $.ajax({
        url           :     '/search/results',
        type          :     'GET',
        contentType   :     'application/json; charset=utf-8',
     })
     .done(function(articles) {

        AddArticleToArray(articles);
        LoadArticleView("");
     });
}

function AddArticleToArray(articles)
{
    if(articles != null)
    {
        articles.forEach(function(article){            
            articleArray.push(article);
        });
    }
}