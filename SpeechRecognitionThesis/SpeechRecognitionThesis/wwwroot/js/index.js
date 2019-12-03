$(document).ready(function() {
    $.getScript("/js/speech_engine.js", function(){
        //LoadArticleEditViewCommands();
        LoadRecommendedArticlesCommands();
        LoadNewestArticlesCommands();
        LoadMostPopularArticlesCommands();
    });
 });

 const recommendedArticleCommandContent = "Poleca ";
 const newestArticleCommandContent = "Nowy ";
 const mostPopularArticleCommandContent = "Popularny ";

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
    //TODO
 }

 function LoadMostPopularArticlesCommands()
 {
    //TODO
 }

function GetNumberOfRecommendedArticles()
{
    var countNumbersOfArticles = $(".most-recommend-articles .most-recommend-article").children().length;
    return countNumbersOfArticles;
}