const artyom = new Artyom();
var articleCategoryArray = null;
var categoriesArray = [];
var articleCategorySidebarCommandArray = [];
var valueOfZoomIn = 1.0;

if( !IsSearchPage() )
{
    initializeArtyom();
}
else
{
    initializeArtyomForSearch();
}

addComandsToArtyom();
AddCategorySidebarCommands();

function initializeArtyomForSearch()
{
    setTimeout(function(){
        artyom.initialize({
            lang:"pl-PL",
            continuous:true,
            listen:true,
            debug:true,
            mode: "normal",
        }).then(() => {
            console.log("Artyom succesfully initialized");
        }).catch((err) => {
            console.log("Artyom couldn't be initialized, please check the console for errors");
            console.log(err);
        });
    },250);
}

function initializeArtyom()
{
    setTimeout(function(){
        artyom.initialize({
            lang:"pl-PL",
            continuous:true,
            listen:true,
            debug:true,
            mode: "quick",
        }).then(() => {
            console.log("Artyom succesfully initialized");
        }).catch((err) => {
            console.log("Artyom couldn't be initialized, please check the console for errors");
            console.log(err);
        });
    },250);
}

function IsSearchPage()
{
    return GetLastPartOfUrlPath() == "search"; 
}

function GetLastPartOfUrlPath()
{
  var urlString = window.location.pathname;
  urlString = urlString.substring( urlString.lastIndexOf('/') + 1 );

  return urlString;
}

function addComandsToArtyom()
{
    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: ["strona główna"],
                action: function(){
                    window.open("/","_self");
                }
            },
            {
                indexes: ["najnowsze"],
                action: function(){
                    window.open("/articles/newest","_self");
                }
            },
            {
                indexes: ["top", "rekomendowane", "polecane", "najczęściej"],
                action: function(){
                    window.open("/articles/top-10","_self");
                }
            },
            {
                indexes: ["dodaj nowy artykuł"],
                action: function(){
                    window.open("/articles/add","_self");
                }
            },
            {
                indexes: ["wyszukiwarka"],
                action: function(){
                    window.open("/search","_self");
                }
            },
            {
                indexes: ["moje artykuły"],
                action: function(){
                    window.open("/articles/my","_self");
                }
            },
            {
                indexes: ["moje konto"],
                action: function(){
                    window.open("/account","_self");
                }
            },
            {
                indexes: ["Zaloguj", "Logowanie"],
                action: function(){
                    window.open("/login","_self");
                }
            },
            {
                indexes: ["Wyloguj", "Wylogowanie"],
                action: function(){
                    window.open("/logout","_self");
                }
            },
            {
                indexes: ["Rejestracja"],
                action: function(){
                    window.open("/register","_self");
                }
            },
            {
                indexes: ["przybliż", "powiększ", "bliżej"],
                action: function(){
                    valueOfZoomIn += 0.5;
                    $('body').css('zoom', valueOfZoomIn );
                }
            },
            {
                indexes: ["oddal", "zmniejsz", "dalej"],
                action: function(){
                    valueOfZoomIn -= 0.5;
                    $('body').css('zoom', valueOfZoomIn );
                }
            },
            {
                indexes: ["normalny widok"],
                action: function(){
                    $('body').css('zoom', "100%" );
                }
            },
            {
                indexes: ["powrót", "poprzednia strona"],
                action: function(){
                    window.history.back();
                }
            },
            {
                indexes: ["powtórz", "następna strona"],
                action: function(){
                    window.history.forward();
                }
            },
            {
                indexes: ["odśwież", "przeładuj stronę"],
                action: function(){
                    location.reload();
                }
            },
            {
                indexes: ["pokaż komendy", "ukryj komendy", "schowaj komendy"],
                action: function(){
                    $(".show-commands-button").click();
                }
            },
            {
                indexes: ["informacja"],
                action: function(){
                    window.open("/Home/About","_self");
                }
            },
        ]);
    }
}

function InsertCharsIntoFocusCtrl(insertCharsToFocusCtrl)
{
    var currentElement = document.activeElement.id;
    var valueElement = "";

    if( currentElement.length > 0 )
    {
        valueElement =  $( "#" + currentElement ).val();
        valueElement += insertCharsToFocusCtrl;
        $( "#" + currentElement ).val(valueElement);
    }
}

function GetFocusCtrlId()
{
    var currentElement = document.activeElement.id;
    return currentElement;
}

function IsSetFocus(idCtrlString)
{
  return (idCtrlString == GetFocusCtrlId() );
}

function LoadSpecialCharactersCommands()
{
    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: ["małpa", "@"],
                action: function(){
                    InsertCharsIntoFocusCtrl("@");
                }
            },
            {
                indexes: ["kropka", "."],
                action: function(){
                    InsertCharsIntoFocusCtrl(".");
                }
            },
        ]);
    }
}

function LoadLettersAndNumbersCommands()
{
    var charValueString = "";
    var indexesCommand = "duże";
    var changeToSmaller = false;
    var indexes = [];

    for(var i =65; i <= 90; i++)
    {
        charValueString = String.fromCharCode(i);

        if( changeToSmaller )
        {
            charValueString = charValueString.toLowerCase()
        }

        indexes.push(indexesCommand + " " + charValueString);

        if( i == 90 ) 
        {
            if( !changeToSmaller )
            {
                i = 64;
                changeToSmaller = true;
                indexesCommand = "małe";
            }
        }
    }

    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: indexes,
                action: function(indexOfArray){
                    var currentElement = document.activeElement.id;
                    var valueElement = "";
                    var codeAsciiValue = indexOfArray;

                    if( currentElement.length > 0 )
                    {
                        valueElement =  $( "#" + currentElement ).val();
                        
                        if( codeAsciiValue > 25 )
                        {
                            codeAsciiValue = (codeAsciiValue % 26) + 32;
                        }

                        valueElement += String.fromCharCode(codeAsciiValue+65);
                        $( "#" + currentElement ).val(valueElement);
                    }
                }
            },
        ]);
    }

    indexes = [];

    for( var j=0; j <= 9; j++)
    {
        var numberValue = j.toString();
        indexes.push("cyfra " + numberValue);
    }

    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: indexes,
                action: function(indexOfArray){      
                    InsertCharsIntoFocusCtrl( indexOfArray.toString() );
                }
            },
        ]);
    }
}

function DeleteCharsFromControlInputString(methodDelete)
{
    var currentElement = document.activeElement.id;
    var valueElement = "";
    var lastIndex = -1;

    if( currentElement.length > 0 )
    {
        valueElement =  $( "#" + currentElement ).val();
        
        if( methodDelete == "char")
        {
            valueElement = valueElement.slice(0, -1);
        }

        else if( methodDelete == "word" )
        {
            lastIndex = valueElement.lastIndexOf(" ");
            valueElement = valueElement.substring(0, lastIndex);
        }

        $( "#" + currentElement ).val(valueElement);
    }

    return valueElement;
}

function LoadCategoryCommandFromDb()
{
  return articleCategoryArray ? articleCategoryArray : LoadCategoryFromDb();
}

function LoadCategoryFromDb()
{
    articleCategoryArray = new Array();

    $.ajax({
        url           :     '/category/results',
        type          :     'GET',
        contentType   :     'application/json; charset=utf-8',
     })
     .done(function(categories) {
        AddCategoryToArray(categories);
     });

     return articleCategoryArray;
}

function AddCategoryToArray(categories)
{
  if(categories != null)
  {
    categoriesArray = categories;

    categories.forEach(function(category){            
      articleCategoryArray.push("Wybierz " + category.name);
      });
  }
}

function AddCategorySidebarCommands()
{
    if( artyom != null 
        && categoriesArray != null )
    {
        if( categoriesArray.length <= 0 )
        {
            LoadCategoryFromDb();
        }

        setTimeout(function(){
            categoriesArray.forEach(function(category){            
                articleCategorySidebarCommandArray.push("Kategoria " + category.name);
            }); 
    
            artyom.addCommands([
                {
                    indexes: articleCategorySidebarCommandArray,
                    action: function( indexOfArray ){
                        $( ".article-categories-list .category-sidebar-element" ).eq( indexOfArray ).click();;
                    }
                },
            ]);
        },500);
    }
}

$(document).on( 'click', '.category-sidebar-element', function(){
    var categoryLinkString = $(this).attr('href');
    window.open( categoryLinkString, "_self" );
});