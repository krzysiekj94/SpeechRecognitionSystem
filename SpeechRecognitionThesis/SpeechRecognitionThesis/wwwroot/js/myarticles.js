$(document).ready(function() {
    $.getScript("/js/speech_engine.js", function(){
        LoadArticleEditViewCommands();
    });
 });

function GetNumberOfArticles()
{
    var countNumbersOfArticles = $(".my-articles .my-article").children().length;
    return countNumbersOfArticles;
}

function GetEditArticleCommandsArray()
{
    var articleCommandsArray = [];
    var countOfArticles = GetNumberOfArticles();

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        articleCommandsArray.push( "Edytuj " + iCounter.toString() );
    }

    return articleCommandsArray;
}

function GetDeleteArticleCommandsArray()
{
    var articleCommandsArray = [];
    var countOfArticles = GetNumberOfArticles();

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        articleCommandsArray.push( "Usuń " + iCounter.toString() );
    }

    return articleCommandsArray;
}

function LoadArticleEditViewCommands()
{
    var editArticleCommandsArray = GetEditArticleCommandsArray();
    var deleteArticleCommandsArray = GetDeleteArticleCommandsArray();

    if( artyom != null )
    {
        artyom.addCommands([
        {
            indexes: editArticleCommandsArray,
            action: function( indexOfArray ){
                $( ".my-articles .my-article .article-edit-form" ).eq( indexOfArray ).submit();
            }
        },
        {
            indexes: deleteArticleCommandsArray,
            action: function( indexOfArray ){
                $( ".my-articles .my-article .delete-article-button" ).eq( indexOfArray ).click();
            }
        },
        ]);
    }
}

$(".delete-article-button").click(function(event){
    
    var valueButton = $(this).val();
    var indexSelector = $('.delete-article-button').index(this);

    $.ajax({
        url         : '/articles/' + valueButton,
        type        : 'DELETE',
        contentType : 'application/json; charset=utf-8',
        statusCode: {
            200: 
                function(){
                    $(".delete-article-button").eq( indexSelector ).closest('.my-article').remove(); 
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Artykuł został pomyślnie usunięty!',
                        showConfirmButton: true,
                        timer: 3000
                      }).then(function(){
                         location.reload();
                      });
                },
            400: 
                function () {
                    Swal.fire({
                        position: 'center',
                        type: 'error',
                        title: 'Wystąpił błąd przy usuwaniu Twojego artykułu! Spróbuj jeszcze raz!',
                        showConfirmButton: true,
                        timer: 3000
                      }).then(function(){
                        //do nothing
                      });
                }
        }
    })
    .done(function(data) {
        console.log("The article was deleted with success!"); 
    });
});