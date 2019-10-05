$(document).ready(function() {
    
    var articlePageCommandsArray = [];

    $( ".see-article-button" ).click(function() {
        var idArticle = $(this).val();
        window.open( "/articles/" + idArticle.toString(), "_self" );
    });

    $.getScript("/js/speech_engine.js", function(){
    
        setTimeout(function(){
            LoadPageVoiceControlCommands();
        },1000);

        /////////////////FUNCTIONS/////////////////

        function LoadPageVoiceControlCommands()
        {
           ReadArticlePageCommandsArray();
       
           if( artyom != null )
           {
               artyom.addCommands([
               {
                   indexes: articlePageCommandsArray,
                   action: function( indexOfArray ){
                       $( ".newest-articles-result .newest-article-result .article-result-info .see-article-button" ).eq( indexOfArray ).click();
                   }
               },
               ]);
           }
        }
       
       function ReadArticlePageCommandsArray()
       {
           var countOfArticles = $(".newest-article-result").length;
       
           for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
           {
                articlePageCommandsArray.push( "Zobacz " + iCounter.toString() );
           }
       }
    });
 });