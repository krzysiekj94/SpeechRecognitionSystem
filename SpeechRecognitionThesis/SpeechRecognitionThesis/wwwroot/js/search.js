const $articlesResultElement = $('.articles-result');
var articleArray = null;
var searchArticleRecognizer = null;

$(document).ready(function() {
    $.getScript("/js/speech_engine.js", function(){
        setTimeout(function(){
            LoadSearchCommands();
            LoadLettersAndNumbersCommandsForSearch();
            LoadSpecialCharactersCommandsForSearch();
        },1000);
    });
 });

 function LoadSearchCommands()
 {
    var searchArticleCommandsArray = GetSearchArticleCommandsArray();

    if( artyom != null )
    {
        artyom.addCommands([
        {
            indexes: searchArticleCommandsArray,
            action: function( indexOfArray ){
                $( ".articles-result .article-result .see-article-button" ).eq( indexOfArray ).click();
            }
        },
        {
            indexes: ["wyczyść frazę", "wyczyść frazy", "wyczyść słowa"],
            action: function(){
              $("#search-text-input").val("");
              $articlesResultElement.empty();
              LoadArticleView("");
            },
        },
        {
            indexes: ["wprowadź frazę", "napisz frazę", "napisz słowa"],
            action: function(){
                artyom.fatality();
                $("#search-text-input").focus(); 
                StartRecognition();
            },
        },
        {
            indexes: ["cofnij"],
            action: function(){
                var currentElement = document.activeElement.id;
                var valueElement = "";
  
                if( currentElement.length > 0 )
                {
                    valueElement =  $( "#" + currentElement ).val();
                    valueElement = valueElement.slice(0, -1);
                    $( "#" + currentElement ).val(valueElement);
                    $articlesResultElement.empty();
                    LoadArticleView(valueElement);
                }
            },
        },
        ]);
    }
 }

 function GetNumberOfArticles()
{
    var countNumbersOfArticles = $(".articles-result").children().length;
    return countNumbersOfArticles;
}

function GetSearchArticleCommandsArray()
{
    var articleSearchCommandsArray = [];
    var countOfArticles = GetNumberOfArticles();

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        articleSearchCommandsArray.push( "Zobacz " + iCounter.toString() );
    }

    return articleSearchCommandsArray;
}

//handlers
$(window).on('load', function()
{
    setTimeout(function(){
        LoadArticleFromDb();
    },500);
});

$("#search-text-input").keyup(function(){
    var searchText = $("#search-text-input").val();
    searchArticleRecognizer.finalSearchContentTranscript = searchText;
    $articlesResultElement.empty();
    LoadArticleView(searchText);
});

$(document).on( 'click', '.see-article-button', function()
{
    var idArticle = $(this).val();
    window.open( "/articles/" + idArticle.toString(), "_self" );
});

//functions
function LoadArticleView(filterValue)
{
    var iCounterAddedArticles = 0;

    filterValue = filterValue.trim();

    if(articleArray != null 
        && articleArray.length > 0)
    {
        articleArray.forEach(function( article ) 
        {
            if( IsCompatibilityWithFilter( article,filterValue ) )
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
                        || article.content.toLowerCase().includes(filterValueLowerCase) 
                        || article.articleCategory.name.toLowerCase().includes(filterValueLowerCase)
                        || article.subject.toLowerCase().includes(filterValueLowerCase) );
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

class SearchArticleSpeechRecognizer {
    constructor() {
        window.SpeechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
        this.finalSearchContentTranscript = $("#search-text-input").val();
        this.recognition = new SpeechRecognition();
        this.letRecognize = true;
        this.InitVariables();
        this.SetCallback();
    }

    InitVariables() {
        this.recognition.interimResults = true;
        this.recognition.maxAlternatives = 10;
        this.recognition.continuous = true;
        this.recognition.lang = "pl-PL"
    }

    StartRecognition(){
        this.recognition.start();
    }

    EndRecognition(){
        this.recognition.stop();    
        initializeArtyom();
    }

    SetCallback(){
        this.recognition.onresult = ( event ) => {

            let interimTranscript = '';
            let eventLength = event.results.length;
            
            for( let i = event.resultIndex; i < eventLength; i++ ) 
            {
              let transcript = event.results[i][0].transcript;
              
              if( event.results[i].isFinal ) 
              {
                if( IsSetFocus("search-text-input") )
                {
                  if( this.letRecognize )
                  {
                    this.finalSearchContentTranscript += transcript;   
                    $articlesResultElement.empty();
                    LoadArticleView(this.finalSearchContentTranscript);
                  }
                }   
              } 
              else 
              {
                interimTranscript += transcript;
              }
            }

            if( this.letRecognize 
             && interimTranscript.toLowerCase().includes("zakończ") 
             && ( interimTranscript.toLowerCase().includes("frazę") 
             || interimTranscript.toLowerCase().includes("szukanie")
             || interimTranscript.toLowerCase().includes("wyszukiwanie") ) )
            {
                this.letRecognize = false;
                this.FixContentTextAfterEndCommand();
                this.EndRecognition();
                $articlesResultElement.empty();
                LoadArticleView( $("#search-text-input").val() );
            }
            else
            {
              if( this.letRecognize && interimTranscript != "" )
              {
                this.AppendRecognizedTextToFocusCtrl(interimTranscript); 
              }
            }
        }
    }

    AppendRecognizedTextToFocusCtrl(interimTranscript)
    {
      var focusCtrlIdString = GetFocusCtrlId();
      var finalContentTranscript = "";
      
      if( focusCtrlIdString == "search-text-input" )
      {
        finalContentTranscript = this.finalSearchContentTranscript;
      }
      
      finalContentTranscript += interimTranscript;

      console.log(finalContentTranscript);
      $("#" + focusCtrlIdString.toString() ).val(finalContentTranscript.toString() ); 
    }

    FixContentTextAfterEndCommand()
    {
      var focusCtrlIdString = GetFocusCtrlId();
      var textAreaString = $("#" + focusCtrlIdString ).val().toLowerCase();   
      var lastIndex = textAreaString.lastIndexOf("zak");
      if( lastIndex > 0)
      {
        textAreaString = textAreaString.substring(0, lastIndex).trim();
        $("#" + focusCtrlIdString ).val(textAreaString);
      }
      else
      {
        console.log("Bad index in FixContentTextAfterEndCommand = " + lastIndex.toString() );
      }
    }
}

function StartRecognition()
{
    searchArticleRecognizer = new SearchArticleSpeechRecognizer();

    if( searchArticleRecognizer != null )
    {
        searchArticleRecognizer.StartRecognition();
    }
}

function LoadSpecialCharactersCommandsForSearch()
{
    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: ["małpa"],
                action: function(){
                    InsertCharsIntoFocusCtrl("@");
                    $articlesResultElement.empty();
                    LoadArticleView( $("#search-text-input").val() + "@" );
                }
            },
            {
                indexes: ["kropka"],
                action: function(){
                    InsertCharsIntoFocusCtrl("." );
                    $articlesResultElement.empty();
                    LoadArticleView( $("#search-text-input").val() + "." );
                }
            },
        ]);
    }
}

function LoadLettersAndNumbersCommandsForSearch()
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
                        $articlesResultElement.empty();
                        LoadArticleView( valueElement );                        
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
                    $articlesResultElement.empty();
                    LoadArticleView(  $("#search-text-input").val() + indexOfArray.toString() );       
                }
            },
        ]);
    }
}