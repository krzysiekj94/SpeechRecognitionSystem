$(document).ready(function() {
    $.getScript("/js/speech_engine.js", function(){
        LoadRecommendedArticlesCommands();
        LoadNewestArticlesCommands();
        LoadMostPopularArticlesCommands();
    });
 });

 const recommendedArticleCommandContent = "Poleca ";
 const newestArticleCommandContent = "Nowy ";
 const mostPopularArticleCommandContent = "Popularne ";

 function LoadRecommendedArticlesCommands()
 {
     var recommendedArticleCommandsArray = [];
     var countOfArticles = GetNumberOfRecommendedArticles(); 

     for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
     {
        recommendedArticleCommandsArray.push( recommendedArticleCommandContent + iCounter.toString() );
     }

     if( artyom != null )
     {
         artyom.addCommands([
         {
             indexes: recommendedArticleCommandsArray,
             action: function( indexOfArray ){
                 $( ".most-recommend-articles .most-recommend-article .recommend-article-form" ).eq( indexOfArray ).submit();
             }
         },
         ]);
     }
 }

 function LoadNewestArticlesCommands()
 {
    var newestArticleCommandsArray = [];
    var countOfArticles = GetNumberOfNewestArticles(); 

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        newestArticleCommandsArray.push( newestArticleCommandContent + iCounter.toString() );
    }

    if( artyom != null )
    {
        artyom.addCommands([
        {
            indexes: newestArticleCommandsArray,
            action: function( indexOfArray ){
                $( ".newest-articles .newest-news-body .newest-article-form" ).eq( indexOfArray ).submit();
            }
        },
        ]);
    }
 }

 function LoadMostPopularArticlesCommands()
 {
    var mostPopularArticleCommandsArray = [];
    var countOfArticles = GetNumberOfMostPopularArticles(); 

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        mostPopularArticleCommandsArray.push( mostPopularArticleCommandContent + iCounter.toString() );
    }

    if( artyom != null )
    {
        artyom.addCommands([
        {
            indexes: mostPopularArticleCommandsArray,
            action: function( indexOfArray ){
                $( ".most-popular-articles .newest-news-body .most-popular-article-form" ).eq( indexOfArray ).submit();
            }
        },
        ]);
    }
 }

function GetNumberOfRecommendedArticles()
{
    var countNumbersOfArticles = $(".most-recommend-articles .most-recommend-article").children().length;
    return countNumbersOfArticles;
}

function GetNumberOfNewestArticles()
{
    var countNumbersOfArticles = $(".newest-articles .newest-news-body").children().length;
    return countNumbersOfArticles;
}

function GetNumberOfMostPopularArticles()
{
    var countNumbersOfArticles = $(".most-popular-articles .newest-news-body").children().length;
    return countNumbersOfArticles;
}