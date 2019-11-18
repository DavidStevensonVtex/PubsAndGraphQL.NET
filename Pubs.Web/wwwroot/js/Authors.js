var url = "/api/graphql";

function DisplayAllAuthors(authors) {
    var html = "";
    html += "<table class='table table-condensed' style='width: 100%'>";
    html += "<thead style='background-color: darkblue; color: white;'><tr><th>Author Id</th><th>Name</th><th style='width: 2in'>Phone</th><th style='width: 4in'>Address</th></tr></thead>";
    html += "<tbody>";

    authors.forEach(function (author, index) {
        html += "<tr>";
        html += "<td>" + author.authorId + "</td>";
        html += "<td>" + author.lastName + ", " + author.firstName + "</td>";
        html += "<td>" + author.phone + "</td>";
        html += "<td>" + author.address + ", " + author.city + ", " + author.state + "  " + author.zip + "</td>";
        html += "</tr>";
        if (author.titles !== undefined) {
            html += "<tr>";
            html += "<td>&nbsp;</td>";
            html += "<td colspan='4'>";

            html += "<table class='table' style='width: 100%'>";
            html += "<thead style='background-color: darkgreen; color: white;'><th>Title</th><th>Type</th><th>Price</th></thead>";
            console.log("Titles are defined");
            author.titles.forEach(function (title, index) {
                html += "<tr>";
                html += "<td style='width: 3.5in'>" + title.title + "</td>";
                html += "<td style='width: 1.5in'>" + title.type + "</td>";
                html += "<td style='width: 0.5in; text-align: right;'>$" + title.price + "</td>";
                html += "</tr>";
            });
            html += "</td>";
            html += "</tr>";
            html+= "</table>";
            html += "</tr>";
        }
    });
    html += "</tbody>";
    html += "</table>";
    $("#authors").html(html);
}

function BuildSelectAuthors(authors) {
    var html = "<option selected disabled>Please select an author</option>";
    authors.forEach(function (author, index) {
        html += "<option value='" + author.authorId + "'>" + author.lastName + ", " + author.firstName + "</option>";
    });
    $("#authorSelect").html(html);
}

function GetAllAuthors(processDataFunction) {
    var withTitles = $('#includeTitles').is(":checked");
    console.log("GetAllAuthors: withTitles: " + withTitles);
    var data = {
        "operationName": "AuthorsQuery",
        "query": `  query AuthorsQuery ($withTitles: Boolean!)
                    { 
                        authors 
                        { 
                            authorId lastName firstName phone address city state zip contract 
                            titles @include(if: $withTitles)
                            {
                                titleId title price pubId type pubDate notes advance royalty ytdSales 
                            }
                        } 
                    }`,
        "variables": {
            "withTitles": withTitles
        }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
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
    var withTitles = $('#includeTitles').is(":checked");
    console.log("GetAuthor: withTitles: " + withTitles);
    var data = {
        "operationName": "AuthorQuery",
        "query": `  query AuthorQuery($authorId: String!, $withTitles: Boolean!)
                    {
                        author(id: $authorId)
                        {
                            authorId lastName firstName phone address city state zip contract
                            titles @include(if: $withTitles)
                            {
                                titleId title price pubId type pubDate notes advance royalty ytdSales 
                            }
                        }
                    }`,
        "variables": {
            "authorId": authorId,
            "withTitles": withTitles
        }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
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
    GetAuthorIdAndName(BuildSelectAuthors);

    $("#allAuthorsButton").click(function () {
        $("#authorSelect")[0].selectedIndex = 0;
        GetAllAuthors(DisplayAllAuthors);
    });

    $("#authorSelect").change(function () {
        var authorId = $(this).val();
        GetAuthor(authorId, DisplaySingleAuthor);
        var author = $("#authorSelect option:selected").text();
        console.log("Selected Author: " + author + "  Selected Author Id: " + authorId);
    });
});