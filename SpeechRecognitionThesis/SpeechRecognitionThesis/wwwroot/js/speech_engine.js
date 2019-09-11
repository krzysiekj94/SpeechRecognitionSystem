const artyom = new Artyom();
initializeArtyom();
addComandsToArtyom();

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
                indexes: ["top", "rekomendowane", "polecane"],
                action: function(){
                    window.open("/articles/top-5","_self");
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
                    artyom.say("Przekierowuję na stronę Twoich artykułów.");
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

function LoadSpecialCharactersCommands()
{
    if( artyom != null )
    {
        artyom.addCommands([
            {
                indexes: ["małpa"],
                action: function(){
                    InsertCharsIntoFocusCtrl("@");
                }
            },
            {
                indexes: ["kropka"],
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