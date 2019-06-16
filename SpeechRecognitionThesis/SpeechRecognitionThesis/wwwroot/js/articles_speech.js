var articleRecognizer = null;

//article textarea handlers
$(window).on('load', function () {
  var articleContent = localStorage.getItem("articleContent");
  $(".article-content").val(articleContent);
});

$(document).ready(function(){ 
  $(".article-content").change(function(){
    
    var contentArticle = $(".article-content").val();
    
    if(contentArticle !== "")
    {
      localStorage.setItem("articleContent", contentArticle);
    }
  }); 
});

$(".article-content").keyup(function(){
  var contentArticle = $(".article-content").val();
    
  if(contentArticle !== "" 
    && articleRecognizer != null)
  {
    articleRecognizer.finalTranscript = contentArticle;
    localStorage.setItem("articleContent", contentArticle);
  }
});

$( ".save-article-button" ).click(function() {
  SaveArticleContentToDatabase();
});

$( ".clear-article-button" ).click(function() {
  localStorage.setItem("articleContent", "");
  $(".article-content").val(localStorage.getItem("articleContent"));

  if(articleRecognizer != null )
  {
    articleRecognizer.finalTranscript = "";
  }
});

//article speech recognizer engine
$.getScript("/js/speech_engine.js", function(){

    artyom.addCommands([
        {
            indexes: ["napisz"],
            action: function(){
                artyom.fatality();
                $(".article-content").focus(); 
                StartRecognition();
            }
        },
        {
            indexes: ["zapisz"],
            action: function(){
              SaveArticleContentToDatabase(); 
            },
        },
        {
          indexes: ["wyczyść"],
          action: function(){
            localStorage.setItem("articleContent", "");
            $(".article-content").val(localStorage.getItem("articleContent"));
          },
      },
    ]);
 
 });

class ArticleSpeechRecognizer {
    constructor() {
        window.SpeechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
        this.finalTranscript = localStorage.articleContent;
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
        
        var contentArticle = $(".article-content").val();
    
        if(contentArticle !== "")
        {
          localStorage.setItem("articleContent", contentArticle);
        }
    }

    SetCallback(){
        this.recognition.onresult = ( event ) => {

            let interimTranscript = '';
            let eventLength = event.results.length;
       
            for (let i = event.resultIndex; i < eventLength; i++) {
             
              let transcript = event.results[i][0].transcript;
             
              if (event.results[i].isFinal) {
                this.finalTranscript += transcript;       
              } else {
                interimTranscript += transcript;
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
              if( this.letRecognize )
              {
                console.log(this.finalTranscript+interimTranscript);
                $(".article-content").val(this.finalTranscript+interimTranscript);    
              }
            }
        }
    }

    FixContentTextAfterEndCommand()
    {
      var textAreaString = $(".article-content").val().toLowerCase();   
      var lastIndex = textAreaString.lastIndexOf("zakończ");
      textAreaString = textAreaString.substring(0, lastIndex);
      $(".article-content").val(textAreaString);
    }

    SaveArticleContentToLocalStorage()
    {
      localStorage.setItem("articleContent", this.finalTranscript);
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


function SaveArticleContentToDatabase()
{
  var articleContentString = $(".article-content").val();

  var insertionDateTime   = GetCurrentDateTimeString();
  var lastUpdateDateTime  = GetCurrentDateTimeString();

  var articleObject = 
  { "AuthorId"        :   1,
    "Content"         :   articleContentString,
    "AuthorName"      :   "Krystian B.",
    "InsertionDate"   :   insertionDateTime,
    "LastUpdateDate"  :   lastUpdateDateTime
  };

  $.ajax({
    url           : '/articles/add',
    type          : 'POST',
    contentType   : 'application/json; charset=utf-8',
    dataType      : 'json',
    headers       : 
    {
        RequestVerificationToken: 
            $('input:hidden[name="__RequestVerificationToken"]').val()
    },
    data: JSON.stringify(articleObject)
  })
  .done(function(result) 
  {
    console.log("/articles/add success");
    console.log("Article ID: " + result);
  });
}

function GetCurrentDateTimeString()
{
  var date = new Date();
  var day = date.getDate();       
  var month = date.getMonth() + 1;    
  var year = date.getFullYear();  
  var hour = date.getHours();
  var minute = date.getMinutes();
  var second = date.getSeconds();

  return date;
}