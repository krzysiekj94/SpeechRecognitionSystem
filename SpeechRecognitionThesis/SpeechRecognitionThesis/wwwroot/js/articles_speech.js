var articleRecognizer = null;
var bIsChanged = false;

//article subject/content/select category handlers
$(window).on('load', function(){
  if( !IsEditArticleMode() )
  {
    var articleContentLocalStorage = localStorage.getItem("articleContent");
    var articleSubjectLocalStorage = localStorage.getItem("articleSubject");
    
    var articleContent = articleContentLocalStorage ? articleContentLocalStorage : "";
    var articleSubject = articleSubjectLocalStorage ? articleSubjectLocalStorage : "";
    $("#article-content").val(articleContent);
    $("#article-subject").val(articleSubject);
  }
});

function IsEditArticleMode()
{
  var modeFromUrlString = GetLastPartOfUrlPath();
  return ( modeFromUrlString == "edit" );
}

function GetArticleIdFromUrl()
{
  var urlString = window.location.pathname;
  urlString = urlString.replace("/edit", ""); 
  urlString = urlString.substring( urlString.lastIndexOf('/') + 1 )

  return urlString;
}

$('#article-subject').on('input', function() {
  var contentSubject = $("#article-subject").val();
    
  if(articleRecognizer != null)
  {
    articleRecognizer.finalArticleSubjectTranscript = contentSubject;
    localStorage.setItem("articleSubject", contentSubject);
    bIsChanged = true;
  }
});

$('#article-content').on('input', function() {
  var contentArticle = $("#article-content").val();
    
  if(articleRecognizer != null)
  {
    articleRecognizer.finalArticleContentTranscript = contentArticle;
    localStorage.setItem("articleContent", contentArticle);
    bIsChanged = true;
  }
});

$( ".save-article-button" ).click(function() {
  SaveArticleContentToDatabase("save");
});

$( ".update-article-button" ).click(function() {
  SaveArticleContentToDatabase("update");
});

$( ".clear-article-button" ).click(function() {
  localStorage.setItem("articleContent", "");
  $("#article-content").val("");
  localStorage.setItem("articleSubject", "");
  $("#article-subject").val("");

  if(articleRecognizer != null )
  {
    articleRecognizer.finalArticleContentTranscript = "";
    articleRecognizer.finalArticleSubjectTranscript = "";
  }
});

//article speech recognizer engine
$.getScript("/js/speech_engine.js", function(){

    var articleCategoryTempArray = LoadCategoryCommandFromDb();
    LoadArticlesBaseCommand();
    LoadLettersAndNumbersCommands();
    LoadSpecialCharactersCommands();
    LoadCategoryCommands();

    function LoadCategoryCommands()
    {
      artyom.addCommands([
        {
            indexes: articleCategoryTempArray,
            action: function(indexOfArray){
              $("#article-category").val( indexOfArray + 1 );
            }
        },
      ]);
    }

    function LoadArticlesBaseCommand()
    {
      artyom.addCommands([
        {
            indexes: ["napisz treść", "napisz tekst"],
            action: function(){
                artyom.fatality();
                $("#article-content").focus(); 
                StartRecognition();
            }
        },
        {
            indexes: ["zapisz artykuł"],
            action: function(){
              if( IsEditArticleMode() )
              {
                SaveArticleContentToDatabase("update"); 
              }
              else
              {
                SaveArticleContentToDatabase("save");
              }
            },
        },
        {
          indexes: ["wyczyść treść", "wyczyść tekst"],
          action: function(){
            localStorage.setItem("articleContent", "");
            $("#article-content").val("");
          },
        },
        {
          indexes: ["wyczyść temat"],
          action: function(){
            localStorage.setItem("articleSubject", "");
            $("#article-subject").val("");
          },
        },
        {
          indexes: ["wyczyść artykuł"],
          action: function(){
            localStorage.setItem("articleContent", "");
            $("#article-content").val("");
            localStorage.setItem("articleSubject", "");
            $("#article-subject").val("");
          },
        },
        {
          indexes: ["napisz temat"],
          action: function(){
              artyom.fatality();
              $("#article-subject").focus(); 
              StartRecognition();
          }
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
              }
          }
        },
    ]);
    }
 });


class ArticleSpeechRecognizer {
    constructor() {
        window.SpeechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
        this.finalArticleContentTranscript = $("#article-content").val();
        this.finalArticleSubjectTranscript = $("#article-subject").val();
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
        
        var contentArticle = $("#article-content").val();
        var subjectArticle = $("#article-subject").val();
    
        if(contentArticle !== "")
        {
          localStorage.setItem("articleContent", contentArticle);
        }

        if( subjectArticle !== "")
        {
          localStorage.setItem("articleSubject", subjectArticle);
        }
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
                if( IsSetFocus("article-content") )
                {
                  this.finalArticleContentTranscript += transcript;  
				          console.log( "Phrase: " + transcript + "confidence: " + event.results[i][0].confidence );
                }
                else if( IsSetFocus("article-subject") )
                {
                  this.finalArticleSubjectTranscript += transcript;
				          console.log( "Phrase: " + transcript + "confidence: " + event.results[i][0].confidence );
                }    
              } 
              else 
              {
                interimTranscript += transcript;
				        console.log( "Phrase: " + transcript + "confidence: " + event.results[i][0].confidence );
              }
            }

            if( this.letRecognize 
             && interimTranscript.toLowerCase().includes("zakończ") 
             && interimTranscript.toLowerCase().includes("artykuł") )
            {
                this.letRecognize = false;
                this.FixContentTextAfterEndCommand();
                this.SaveArticleContentToLocalStorage();
                this.EndRecognition();
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
      
      if( focusCtrlIdString == "article-content" )
      {
        finalContentTranscript = this.finalArticleContentTranscript;
      }
      else if( focusCtrlIdString == "article-subject" )
      {
        finalContentTranscript = this.finalArticleSubjectTranscript;
      }
      
      if( !bIsChanged )
      {
        finalContentTranscript += interimTranscript;
      }
      else
      {
        bIsChanged = false;
      }

      console.log(finalContentTranscript);
      $("#" + focusCtrlIdString.toString() ).val(finalContentTranscript.toString() ); 
    }

    IsStringContainAnotherAtEnd( firstString, secondString) 
    {
        var bIsStringContainAnotherAtEnd = false;

        firstString = firstString.trim();
        secondString = secondString.trim();
        
        var indexOfBeginSecondString = firstString.lastIndexOf(secondString);

        if( indexOfBeginSecondString > 0 
          && firstString.substring(indexOfBeginSecondString) == secondString )
        {
          bIsStringContainAnotherAtEnd = true;
        }

        return bIsStringContainAnotherAtEnd;
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

    SaveArticleContentToLocalStorage()
    {
      this.finalArticleContentTranscript = $("#article-content").val();
      this.finalArticleSubjectTranscript = $("#article-subject").val();
      
      localStorage.setItem("articleContent", this.finalArticleContentTranscript);
      localStorage.setItem("articleSubject", this.finalArticleSubjectTranscript);
    }
}

function StartRecognition()
{
    articleRecognizer = new ArticleSpeechRecognizer();

    if( articleRecognizer != null )
    {
        articleRecognizer.StartRecognition();
    }
}

function SaveArticleContentToDatabase(stateString)
{
  var articleContentString = $("#article-content").val();
  var articleSubjectString = $("#article-subject").val();
  var articleCategoryString = $("#article-category").val();
  var urlAction = "";
  var idFromUrl = "";
  var methodAction = "";

  var articleObject = 
  { 
    "Subject"               :   articleSubjectString,
    "Content"               :   articleContentString,
    "ArticleCategoryRefId"  :   articleCategoryString,
  };

  if( stateString == "save" )
  {
    urlAction = "/articles/add";
    methodAction = "POST";
  }
  else if( stateString == "update" )
  {
    idFromUrl = GetArticleIdFromUrl();
    urlAction = "/articles/" + idFromUrl;
    methodAction = "PUT";
  }

  $.ajax({
    url           : urlAction,
    type          : methodAction,
    contentType   : 'application/json; charset=utf-8',
    dataType      : 'json',
    headers       : 
    {
        RequestVerificationToken: 
            $('input:hidden[name="__RequestVerificationToken"]').val()
    },
    data: JSON.stringify(articleObject),
    statusCode: {
      200: 
          function(responseObject){
              Swal.fire({
                  position: 'center',
                  type: 'success',
                  title: 'Artykuł został pomyślnie dodany/zaktualizowany!',
                  showConfirmButton: true,
                  timer: 3000
                }).then(function(){
                  if( stateString == "save" )
                  {
                    window.open( "/articles/" + responseObject.toString(), "_self" );
                  }
                });
          },
      400: 
          function () {
              Swal.fire({
                  position: 'center',
                  type: 'error',
                  title: 'Wystąpił błąd przy dodawaniu/aktualizacji artykułu! Sprawdź poprawność wpisanych danych i spróbuj jeszcze raz',
                  showConfirmButton: true,
                  timer: 3000
                }).then(function(){
                  //do nothing
                });
          }
      }
  })
  .done(function(result) 
  {
    console.log("/articles/add success");
    console.log("Article ID: " + result);
  });
}