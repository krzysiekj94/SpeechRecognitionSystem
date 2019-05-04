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
        {
          indexes: ["zapisz artykuł"],
          action: function(){
              //TODO save into database
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