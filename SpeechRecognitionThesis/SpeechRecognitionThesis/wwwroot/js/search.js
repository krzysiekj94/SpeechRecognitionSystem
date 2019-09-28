const $articlesResultElement = $('.articles-result');
var articleArray = null;
var searchArticleRecognizer = null;
var dayOfMonthCalenderPrefix19CommandArray = [ "pierwszy", "drugi", "trzeci", "czwarty", "piąty", "szósty", "siódmy", "ósmy", "dziewiąty" ];
var dayOfMonthCalender = dayOfMonthCalenderPrefix19CommandArray;
var monthsCalender = [ "styczeń", "luty", "marzec", "kwiecień", "maj", "czerwiec", "lipiec", "sierpień", "wrzesień", "październik", "listopad", "grudzień" ];
var yearsArray = [];
var actualDateFrom = null;
var actualDateTo = null;
var articleCategoryArray = null;
var searchArticleCommandsArray = null;

$(document).ready(function() {
    
    $.datepicker.regional[ "pl" ];
    
    $('#datepicker-article-from').datepicker(
        {
        onSelect: function( date, datepicker ) {
            
            var dateFromPLFormatArray = GetDateArrayFromPLFormat( date );
            actualDateFrom = new Date( dateFromPLFormatArray[2], dateFromPLFormatArray[1]-1 ,dateFromPLFormatArray[0] );
            LoadArticleViewWithKeywords();                
        },
    });

    $('#datepicker-article-to').datepicker(
        {
        onSelect: function( date, datepicker ) {
            
            var dateFromPLFormatArray = GetDateArrayFromPLFormat( date );
            actualDateTo = new Date( dateFromPLFormatArray[2], dateFromPLFormatArray[1]-1 ,dateFromPLFormatArray[0] );
            LoadArticleViewWithKeywords();                
        },
    });

    $.getScript("/js/speech_engine.js", function(){
        
        var articleCategoryArray = LoadCategoryCommandFromDb();
        InitDateInfo();

        setTimeout(function(){
            LoadCategoryCommands();
            LoadSearchCommands();
            LoadLettersAndNumbersCommandsForSearch();
            LoadSpecialCharactersCommandsForSearch();
            PrepareDayOfMonthCalenderCommands();
            PrepareMonthsCalenderCommands();
            PrepareYearsCalenderCommands();
            LoadArticleView("");
        },1500);

        function LoadCategoryCommands()
        {
            articleCategoryArray.push( "Wybierz wszystkie" );
            AddAllCategoriesFilterToSelectList( articleCategoryArray.length );
            
            artyom.addCommands([
            {
                indexes: articleCategoryArray,
                action: function(indexOfArray){
                    $("#search-article-category").val( indexOfArray + 1 );

                    var searchText = $("#search-text-input").val();
                    $articlesResultElement.empty();
                    LoadArticleView( searchText );
                }
            },
            ]);

            return articleCategoryArray;
        }
    });
 });

 function InitDateInfo()
 {
    actualDateFrom = new Date();
    actualDateFrom.setFullYear( actualDateFrom.getFullYear() - 1 );
    actualDateTo = new Date();

    $("#datepicker-article-from").datepicker('setDate', new Date(  actualDateFrom.getFullYear(), actualDateFrom.getMonth(), actualDateFrom.getDate() ) ); 
    $("#datepicker-article-to").datepicker('setDate', new Date(  actualDateTo.getFullYear(), actualDateTo.getMonth(), actualDateTo.getDate() ) ); 
 }

 function PrepareRestOfMonthDays()
 {
    for(var iDay = 10; iDay <= 31; iDay++ )
    {
       dayOfMonthCalender.push( iDay.toString() );
    }
 }

 function PrepareDayOfMonthCalenderCommands()
 {
     PrepareRestOfMonthDays();

    artyom.addCommands([
    {
        indexes: dayOfMonthCalender,
        action: function( indexOfArray ){
            
            var valueOfMonthDayFromArray = isNaN( dayOfMonthCalender[ indexOfArray ] ) ? indexOfArray+1 : dayOfMonthCalender[ indexOfArray ];
            
            if( IsSetFocus("datepicker-article-from") )
            {
                actualDateFrom.setDate( valueOfMonthDayFromArray );
                actualDateFrom.setHours( 0,0,0,0 );
                $("#datepicker-article-from").datepicker('setDate', new Date(  actualDateFrom.getFullYear(), actualDateFrom.getMonth(), actualDateFrom.getDate() ) ); 
            }
            else if( IsSetFocus("datepicker-article-to") )
            {
                actualDateTo.setDate( valueOfMonthDayFromArray );
                actualDateTo.setHours( 0,0,0,0 );
                $("#datepicker-article-to").datepicker('setDate', new Date(  actualDateTo.getFullYear(), actualDateTo.getMonth(), actualDateTo.getDate() ) ); 
            }

            LoadArticleViewWithKeywords();
        }
    },
    ]);
 }

 function PrepareMonthsCalenderCommands()
 {
    artyom.addCommands([
        {
            indexes: monthsCalender,
            action: function( indexOfArray ){
                
                if( IsSetFocus("datepicker-article-from") )
                {
                    actualDateFrom.setMonth( indexOfArray );
                    $("#datepicker-article-from").datepicker('setDate', new Date(  actualDateFrom.getFullYear(), actualDateFrom.getMonth(), actualDateFrom.getDate() ) ); 
                }
                else if( IsSetFocus("datepicker-article-to") )
                {
                    actualDateTo.setMonth( indexOfArray );
                    $("#datepicker-article-to").datepicker('setDate', new Date(  actualDateTo.getFullYear(), actualDateTo.getMonth(), actualDateTo.getDate() ) ); 
                }

                LoadArticleViewWithKeywords();
            }
        },
        ]);
 }

 function LoadYearsInfo()
 {
     var iEndYear = actualDateTo.getFullYear();

     for( var iYear = 1970; iYear <= iEndYear; iYear++ )
     {
        yearsArray.push( iYear.toString() );
     }
 }

 function PrepareYearsCalenderCommands()
 {
    LoadYearsInfo();

    artyom.addCommands([
        {
            indexes: yearsArray,
            action: function( indexOfArray ){
                                
                if( IsSetFocus("datepicker-article-from") )
                {
                    actualDateFrom.setFullYear( yearsArray[ indexOfArray ] );
                    $("#datepicker-article-from").datepicker('setDate', new Date(  actualDateFrom.getFullYear(), actualDateFrom.getMonth(), actualDateFrom.getDate() ) ); 
                }
                else if( IsSetFocus("datepicker-article-to") )
                {
                    actualDateTo.setFullYear( yearsArray[ indexOfArray ] );
                    $("#datepicker-article-to").datepicker('setDate', new Date(  actualDateTo.getFullYear(), actualDateTo.getMonth(), actualDateTo.getDate() ) ); 
                }

                LoadArticleViewWithKeywords();
            }
        },
        ]);
 }

 function LoadSearchCommands()
 {
    GetSearchArticleCommandsArray();

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
            indexes: ["wyczyść", "usuń"],
            action: function(){
              $("#search-text-input").val("");
              $articlesResultElement.empty();
              LoadArticleView("");
            },
        },
        {
            indexes: ["wyszukaj", "wprowadź słow", "napisz słow"],
            action: function(){
                artyom.fatality();
                $("#search-text-input").focus(); 
                StartRecognition();
            },
        },
        {
            smart: true,
            indexes: ["cofnij *"],
            action: function( i, wildcard ){
                
                var letterRegexString = RegExp('lit[\\S]+');
                var wordRegexString = RegExp('sło[\\S]+');
                var valueFilterArticle = "";

                if( letterRegexString.test( wildcard ) )
                {
                    valueFilterArticle = DeleteCharsFromControlInputString("char");
                }
                else if( wordRegexString.test( wildcard ) )
                {
                    valueFilterArticle = DeleteCharsFromControlInputString("word");
                }

                if( valueFilterArticle.length > 0 )
                {
                    $articlesResultElement.empty();
                    LoadArticleView(valueFilterArticle);
                }
            },
        },
        {
            smart: true,
            indexes: ["data *", "ustaw datę *"],
            action: function( i, wildcard ){
                
                var createDateFrom = RegExp('po[\\S]*');
                var createdDateTo = RegExp('ko[\\S]*');
                
                if( wildcard == "od" || createDateFrom.test( wildcard ) )
                {
                    $("#datepicker-article-from").datepicker('show'); 
                }
                else if( wildcard == "do" || createdDateTo.test( wildcard ) )
                {
                    $("#datepicker-article-to").datepicker('show');
                }
            },
        },
        {
            indexes: ["ukryj datę"],
            action: function(){
                if( IsSetFocus("datepicker-article-from") )
                {
                    $("#datepicker-article-from").datepicker('hide'); 
                }
                else if( IsSetFocus("datepicker-article-to") )
                {
                    $("#datepicker-article-to").datepicker('hide');
                }
            },
        },
        ]);
    }
 }

function GetSearchArticleCommandsArray()
{
    searchArticleCommandsArray = [];
    var countOfArticles = articleArray.length;

    for( var iCounter = 1; iCounter <= countOfArticles; iCounter++ )
    {
        searchArticleCommandsArray.push( "Zobacz " + iCounter.toString() );
    }

    return searchArticleCommandsArray;
}

//handlers
$(window).on('load', function()
{
    setTimeout(function(){
        LoadArticleFromDb();
    },800);
});

$("#search-text-input").keyup(function(){
    var searchText = $("#search-text-input").val();
    
    if( searchArticleRecognizer != null )
    {
        searchArticleRecognizer.finalSearchContentTranscript = searchText;
    }

    $articlesResultElement.empty();
    LoadArticleView(searchText);
});

$(document).on( 'click', '.see-article-button', function()
{
    var idArticle = $(this).val();
    window.open( "/articles/" + idArticle.toString(), "_self" );
});

$('#search-article-category').on('change', function() {
    LoadArticleViewWithKeywords();
  });

//functions

function LoadArticleViewWithKeywords()
{
    var searchText = $("#search-text-input").val();
    $articlesResultElement.empty();
    LoadArticleView(searchText);
}

function LoadArticleView(filterValue)
{
    var iCounterAddedArticles = 0;

    filterValue = filterValue.trim();

    if(articleArray != null 
        && articleArray.length > 0)
    {


        articleArray.forEach(function( article ) 
        {
            if( IsProperCategory( article.articleCategory.name ) 
            && IsSetProperlyDateFilter( article.articleModificationDate )
            && IsCompatibilityWithFilter( article,filterValue ) )
            {
                iCounterAddedArticles++;
                $articlesResultElement.append( GetArticleHtmlElement( article, iCounterAddedArticles ) ); 
            }      
        });
    }

    if( iCounterAddedArticles <= 0 )
    {
        $articlesResultElement.append( GetEmptyArticlesListMessageString() );
    }
}

function GetEmptyArticlesListMessageString()
{
    return '<li class="list-group-item article-result"><h4><b>Brak artykułów spełniających kryteria wyszukiwania!</b></h4></li>';
}

function GetArticleHtmlElement( article, iCounterAddedArticles )
{
   var articleResultHtml = '<li class="list-group-item article-result">'
                            + '<div class="row">'
                            +   '<div class="col-sm-3 text-center">'
                            +        '<h3>' + article.articleCategory.name + '</h3>'
                            +        '<img src="' + GetCategoryImage( article.articleCategory.name ) + '" class="img-circle" height="120" width="120" alt="Ikonka kategorii">'
                            +   '</div>'
                            + '<div class="col-sm-9 article-result-info">'
                            +   '<p><h4><b>Temat:</b> '                   + article.subject                             + '</h4></p>'
                            +   '<p><h4><b>Treść:</b> '                   + CutTextOverLimit( article.content, 500 )    + '</h4></p>'
                            +   '<p><h4><b>Data ost. modyfikacji:</b> '   + article.articleModificationDate             + '</h4></p>'
                            +   '<p></p>'
                            +   '<p><h4>Komenda do przekierowania do strony artykułu: <b>\"Zobacz ' +  iCounterAddedArticles + '\"</b></h4></p>'
                            +   '<button class="see-article-button btn-success" type="submit" value="' + article.id + '"><h4>Zobacz więcej (' + iCounterAddedArticles + ')</h4></button>'
                          +   '</div>'  
                          +  '</div>' 
                         + '</li>';

    return articleResultHtml;
}

function CutTextOverLimit( modifyString, charsLimit )
{
    if( modifyString.length > charsLimit )
    {
        modifyString = modifyString.substring(0,charsLimit-1);
        modifyString += "...";
    }

    return modifyString;
}

function GetCategoryImage( categoryNameString )
{
    var categoryImagePathString = '/images/category/';

    switch( categoryNameString ) {
        case "Sport":
            categoryImagePathString += "ball.jpg";
            break;
        case "Nauka":
            categoryImagePathString += "science.jpg";
            break;
        case "Świat":
            categoryImagePathString += "world.jpg";
            break;
        case "Kraj":
            categoryImagePathString += "poland.jpg";
            break;
        case "Popularnonaukowe":
            categoryImagePathString += "popular_science.jpg";
            break;
        default:
            categoryImagePathString = "";
            break;
      }

    return categoryImagePathString;
}

function GetNumberWithoutSuffixZero( numberWithSuffixZeroString )
{
    var numberValueString = numberWithSuffixZeroString;

    if( numberValueString.charAt( 0 ) == '0' )
    {
        numberValueString = numberValueString.substring( 1,numberValueString.length )
    }

    return numberValueString;
}

function GetDateTimeArrayFromPLFormat( datePLFormatString )
{
    var lastIndex = datePLFormatString.lastIndexOf(" ");
    var tempValueArticleDate = datePLFormatString.substring( 0, lastIndex ).toString();
    var dateStringArray = tempValueArticleDate.split(".");

    return dateStringArray;
} 

function GetDateArrayFromPLFormat( datePLFormatString )
{
    return datePLFormatString.split(".");
} 

function IsSetProperlyDateFilter( createArticleDateString )
{
    var dateFromPLFormatArray = GetDateTimeArrayFromPLFormat( createArticleDateString );
    var createArticleDate = null;
    var bIsSetProperDateFilter = false;

    if( dateFromPLFormatArray.length == 3 )
    {
        createArticleDate = new Date( dateFromPLFormatArray[2], dateFromPLFormatArray[1]-1 ,dateFromPLFormatArray[0] );

        if( createArticleDate != null 
            && actualDateFrom != null 
            && actualDateTo != null
            && actualDateFrom <= createArticleDate
            && actualDateTo >= createArticleDate )
        {
            bIsSetProperDateFilter = true;
        }
    }

    return bIsSetProperDateFilter;
}

function IsProperCategory( categoryName )
{
    var bIsProperCategory = false;
    var valueOfSelectedCategory = parseInt( $('#search-article-category').val() );
    var indexArrayOfSelectedCategory = valueOfSelectedCategory - 1;
    categoryName = categoryName.toLowerCase();

    if( valueOfSelectedCategory == 6 
        || ( indexArrayOfSelectedCategory < articleCategoryArray.length 
            && indexArrayOfSelectedCategory > -1 
            && ( articleCategoryArray[ indexArrayOfSelectedCategory ].toLowerCase().includes( categoryName ) ) ) )
    {
        bIsProperCategory = true;
    }

    return bIsProperCategory;
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

function AddAllCategoriesFilterToSelectList( iSizeArticleCategoryList )
{
    var allCategoryOptionText = 'Wszystkie kategorie'; 
    var allCategoryOptionValue = iSizeArticleCategoryList; 

    $('#search-article-category').append( '<option value="' + allCategoryOptionValue + '">' + allCategoryOptionText + '</option>' );
    $('#search-article-category').val( allCategoryOptionValue );
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