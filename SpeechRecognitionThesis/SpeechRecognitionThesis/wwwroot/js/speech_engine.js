const artyom = new Artyom();

setTimeout(function(){// if you use artyom.fatality , wait 250 ms to initialize again.
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
        indexes: ["top", "rekomendowane", "polecane"],
        action: function(){
            window.open("/articles/top-5","_self");
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
        indexes: ["dane konta"],
        action: function(){
            window.open("/articles/newest","_self");
        }
    },
]);