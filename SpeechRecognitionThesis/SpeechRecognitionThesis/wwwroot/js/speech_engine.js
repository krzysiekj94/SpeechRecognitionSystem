const artyom = new Artyom();

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

artyom.addCommands([
    {
        indexes: ["najnowsze"],
        action: function(){
            artyom.say("Przekierowuję na stronę najnowszych artykułów.");
        }
    },
    {
        indexes: ["top"],
        action: function(){
            artyom.say("Przekierowuję na stronę top 10 artykułów.");
        }
    },
    {
        indexes: ["wyszukiwarka"],
        action: function(){
            artyom.say("Przekierowuję na stronę wyszukiwarki artykułów.");
        }
    },
    {
        indexes: ["moje artykuły"],
        action: function(){
            artyom.say("Przekierowuję na stronę Twoich artykułów.");
        }
    },
    {
        indexes: ["dodaj nowy artykuł"],
        action: function(){
            artyom.say("Przekierowuję na stronę dodawania nowego artykułu");
        }
    },
    {
        indexes: ["dane konta", "dane mojego konta"],
        action: function(){
            artyom.say("Przekierowuję do strony edycji Twojego konta!");
        }
    },
]);