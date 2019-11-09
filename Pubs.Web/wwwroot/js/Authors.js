var url = "/api/graphql";

function Startup(authors) {
    // DisplayAllAuthors(authors);
    BuildSelectAuthors(authors);
}

function DisplayAllAuthors(authors) {
    // console.log("DisplayAllAuthors");
    // console.log(authors);
    var html = "<table class='table-dark' style='width: 100%'>";
    html += "<thead><tr><th>Author Id</th><th>Name</th><th style='width: 2in'>Phone</th><th style='width: 4in'>Address</th></tr></thead>";
    html += "<tbody>";
    authors.forEach(function (author, index) {
        html += "<tr>";
        html += "<td>" + author.authorId + "</td>";
        html += "<td>" + author.lastName + ", " + author.firstName + "</td>";
        html += "<td>" + author.phone + "</td>";
        html += "<td>" + author.address + ", " + author.city + ", " + author.state + "  " + author.zip + "</td>";
        html += "</tr>";
    });
    html += "</tbody>";
    html += "</table>";
    $("#authors").html(html);
}

function BuildSelectAuthors(authors) {
    // console.log("BuildSelectAuthors");
    // console.log(authors);
    var html = "<option selected disabled>Please select an author</option>";
    authors.forEach(function (author, index) {
        html += "<option value='" + author.authorId + "'>" + author.lastName + ", " + author.firstName + "</option>";
    });
    $("#authorSelect").html(html);
}

function GetAllAuthors(processDataFunction) {
    var data = {
        "operationName": "AuthorsQuery",
        "query": "query AuthorsQuery { authors { authorId lastName firstName phone address city state zip contract } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // console.log("success!");
            processDataFunction(data.data.authors);
        },
        error: function (data) {
            console.log("error!");
            console.log(data);
        },
        contentType: 'application/json'
    });
}

function GetAuthorIdAndName(processDataFunction) {
    var data = {
        "operationName": "AuthorsQuery",
        "query": "query AuthorsQuery { authors { authorId lastName firstName } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            console.log("success!");
            processDataFunction(data.data.authors);
        },
        error: function (data) {
            console.log("error!");
            console.log(data);
        },
        contentType: 'application/json'
    });
}

function DisplaySingleAuthor(author) {
    var authors = [author];
    DisplayAllAuthors(authors);
}

function GetAuthor(authorId, processDataFunction) {
    // console.log("GetAuthor: " + authorId);
    var variables = { "authorId": authorId };
    var data = {
        "operationName": "AuthorQuery",
        "query": "query AuthorQuery($authorId: String!) { author(id: $authorId)  { authorId lastName firstName phone address city state zip contract } }",
        "variables": { "authorId": authorId }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            //console.log("GetAuthor: success!");
            processDataFunction(data.data.author);
        },
        error: function (data) {
            console.log("error!");
            console.log(data);
        },
        contentType: 'application/json'
    });
}


$(document).ready(function () {
    console.log("ready!");

    GetAllAuthors(Startup);
    // GetAllAuthors(DisplayAllAuthors);
    GetAuthorIdAndName(BuildSelectAuthors);

    $("#allAuthorsButton").click(function () {
        // console.log("allAuthorsButton clicked");
        $("#authorSelect")[0].selectedIndex = 0;
        GetAllAuthors(DisplayAllAuthors);
    });

    $("#authorSelect").change(function () {
        // console.log("Author Selected");
        var authorId = $(this).val();
        GetAuthor(authorId, DisplaySingleAuthor);
        var author = $("#authorSelect option:selected").text();
        console.log("Selected Author: " + author + "  Selected Author Id: " + authorId);
    });
});