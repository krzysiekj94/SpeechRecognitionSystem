var articleCategoryCommandArray = [];

$(document).ready(function() {
    
    $.getScript("/js/speech_engine.js", function(){
    
        articleCategoryCommandArray = LoadCategoryCommandFromDb();

        setTimeout(function(){
            LoadCategoryCommands();
        },1500);

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