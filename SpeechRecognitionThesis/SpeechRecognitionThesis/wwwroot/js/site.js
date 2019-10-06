articleCategorySidebarArray = [];

$(document).ready(function() {
    
    LoadCategoryListFromDb();

    setTimeout(function(){
        InitCategorySidebarView();
    },250);

});

function InitCategorySidebarView()
{
    articleCategorySidebarArray.forEach(function(category){            
        $(".article-categories-list").append( '<a href="/articles/category/' + category.id 
                                                + '" class="list-group-item sidebar-element category-sidebar-element">' + category.name + '</a>' );
    });
}

function LoadCategoryListFromDb()
{
    articleCategorySidebarArray = new Array();

    $.ajax({
        url           :     '/category/results',
        type          :     'GET',
        contentType   :     'application/json; charset=utf-8',
     })
     .done(function(categories) {
        articleCategorySidebarArray = categories;
     });
}