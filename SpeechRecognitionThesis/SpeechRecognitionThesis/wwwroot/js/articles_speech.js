$.getScript("/js/speech_engine.js", function(){

    artyom.addCommands([
        {
            indexes: ["pisz artykuł"],
            action: function(){
                console.log("piszę artykuł");
            }
        },
    ]);
 
 });