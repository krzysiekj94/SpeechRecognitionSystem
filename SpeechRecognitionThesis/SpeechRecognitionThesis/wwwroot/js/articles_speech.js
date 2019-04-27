$.getScript("/js/speech_engine.js", function(){

    artyom.addCommands([
        {
            indexes: ["napisz artykuł"],
            action: function(){
                console.log("piszę artykuł");
            }
        },
    ]);
 
 });