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

function GetArticleCommandsArray()
{
    var articleCommandsArray = [];
    var countOfArticles = GetNumberOfArticles();

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        articleCommandsArray.push( "Edytuj " + iCounter.toString() );
    }

    return articleCommandsArray;
}

function LoadArticleEditViewCommands()
{
    var articleCommandsArray = GetArticleCommandsArray();

    if( artyom != null )
    {
        artyom.addCommands([
        {
            indexes: articleCommandsArray,
            action: function( indexOfArray ){
                $( ".my-articles .my-article .article-form" ).eq( indexOfArray ).submit();
            }
        },
        ]);
    }
}