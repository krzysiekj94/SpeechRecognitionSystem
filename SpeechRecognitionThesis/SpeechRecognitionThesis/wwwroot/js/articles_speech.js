var articleRecognizer = null;

$.getScript("/js/speech_engine.js", function(){

    artyom.addCommands([
        {
            indexes: ["napisz artykuł"],
            action: function(){
                artyom.fatality();
                StartRecognition();
            }
        },
    ]);
 
 });

class ArticleSpeechRecognizer {
    constructor() {
        window.SpeechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
        this.finalTranscript = '';
        this.recognition = new SpeechRecognition();
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
            let style = '<i style="color:#ddd;">';
       
            for (let i = event.resultIndex; i < eventLength; i++) {
             
              let transcript = event.results[i][0].transcript;
             
              if( transcript.includes("czerwony"))
              {
                style = '<i style="color:red;">';
                document.querySelector("body").style.backgroundColor = "red";
              }
       
              if( transcript.includes("niebieski"))
              {
                style = '<i style="color:blue;">';
                document.querySelector("body").style.backgroundColor = "blue";
              }
       
              if( transcript.includes("zielony"))
              {
                style = '<i style="color:green;">';
                document.querySelector("body").style.backgroundColor = "green";
              }
       
              if( transcript.includes("żółty"))
              {
                style = '<i style="color:yellow;">';
                document.querySelector("body").style.backgroundColor = "yellow";
              }
       
              if( transcript.includes("pomarańczowy"))
              {
                style = '<i style="color:orange;">';
                document.querySelector("body").style.backgroundColor = "orange";
              }
             
              if (event.results[i].isFinal) {
                this.finalTranscript += transcript;       
              } else {
                interimTranscript += transcript;
              }
            }
            
            console.log(this.finalTranscript+interimTranscript);
            $(".article-content").val(this.finalTranscript+interimTranscript);     
            

            if( interimTranscript.toLowerCase().includes("teraz")
             && interimTranscript.toLowerCase().includes("zakończ") 
             && interimTranscript.toLowerCase().includes("artykuł") )
            {
                this.EndRecognition();
            }
        }
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