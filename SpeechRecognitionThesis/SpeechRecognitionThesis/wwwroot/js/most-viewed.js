var articlePageCommandsArray = [];
var articleCategoryCommandArray = [];

$(document).ready(function() {
    
    $( ".see-article-button" ).click(function() {
        var idArticle = $(this).val();
        window.open( "/articles/" + idArticle.toString(), "_self" );
    });

    $.getScript("/js/speech_engine.js", function(){
    
        articleCategoryCommandArray = LoadCategoryCommandFromDb();

        setTimeout(function(){
            LoadCategoryCommands();
            LoadPageVoiceControlCommands();
        },1500);

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
                       $( ".most-viewed-articles-result .most-viewed-article-result .article-result-info .see-article-button" ).eq( indexOfArray ).click();
                   }
               },
               ]);
           }
        }
       
       function ReadArticlePageCommandsArray()
       {
           var countOfArticles = $(".most-viewed-article-result").length;
       
           for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
           {
                articlePageCommandsArray.push( "Zobacz " + iCounter.toString() );
           }
       }

       function LoadCategoryCommands()
       {   
           if( artyom != null )
           {
                artyom.addCommands([
                {
                    indexes: articleCategoryCommandArray,
                    action: function(indexOfArray){
                         var idCategory = categoriesArray[ indexOfArray ];
                         window.open( "/articles/category/" + idCategory.id.toString(), "_self" );
                    }
                },
                ]);
           }      
       }
    });
 });