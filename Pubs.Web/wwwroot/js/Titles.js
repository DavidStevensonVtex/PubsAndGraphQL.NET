var url = "/api/graphql";

function DisplayAllTitles(titles) {
    var html = "";

    html += "<table class='table table-condensed' style='width: 100%'>";
    html += "<thead style='background-color: darkblue; color: white;'><th>Title Id</th><th>Title</th><th>Price</th><th>Type</th><th>Pub Date</th></thead>";
    html += "<tbody>";
    titles.forEach(function (title, index) {
        var price;
        if (title.price === null) {
            price = "";
        }
        else {
            price = "$" + title.price.toFixed(2);
        }
        html += "<tr>";
        html += "<td>" + title.titleId + "</td>";
        html += "<td>" + title.title + "</td>";
        html += "<td style='text-align: right;'>" + price + "</td>";
        html += "<td>" + title.type + "</td>";
        html += "<td>" + title.pubDate + "</td>";
        html += "</tr>";

        // Note: There is only one publisher for a title.
        if (title.publisher !== undefined) {
            var publisher = title.publisher;
            html += "<tr>";
            html += "<td>&nbsp;</td>";
            html += "<td colspan='4'>";

            html += "<table class='table table-condensed' style='width: 100%'>";
            html += "<thead style='background-color: darkred; color: white;'><th>Pub Id</th><th>Name</th><th>City</th><th>State</th><th>Country</th></thead>";
            html += "<tbody>";
            html += "<tr>";
            html += "<td>" + publisher.pubId + "</td>";
            html += "<td>" + publisher.name + "</td>";
            html += "<td>" + publisher.city + "</td>";
            html += "<td>" + publisher.state + "</td>";
            html += "<td>" + publisher.country + "</td>";
            html += "</tr>";
            html += "</tbody>";
            html += "</table>";

            html += "</td>";
            html += "</tr>";
        }

        if (title.authors !== undefined) {
            html += "<tr>";
            html += "<td>&nbsp;</td>";
            html += "<td colspan='4'>";

            html += "<table class='table table-condensed' style='width: 100%'>";
            html += "<thead style='background-color: darkgreen; color: white;'><th>Author Id</th><th>Name</th><th>Phone</th><th>Address</th></thead>";
            html += "<tbody>";
            html += "<tr>";

            title.authors.forEach(function (author, index) {
                html += "<tr>";
                html += "<td>" + author.authorId + "</td>";
                html += "<td>" + author.lastName + ", " + author.firstName + "</td>";
                html += "<td>" + author.phone + "</td>";
                html += "<td>" + author.address + ", " + author.city + ", " + author.state + "  " + author.zip + "</td>";
                html += "</tr>";
            });

            html += "</tr>";
            html += "</tbody>";
            html += "</table>";

            html += "</td>";
            html += "</tr>";

        }
    });
    html += "</tbody>";
    html += "</table>";
    $("#titles").html(html);
}

function BuildSelectTitles(titles) {
    var html = "<option selected disabled>Please select an title</option>";
    titles.forEach(function (title, index) {
        html += "<option value='" + title.titleId + "'>" + title.title + "</option>";
    });
    $("#titleSelect").html(html);
}

function GetAllTitles(processDataFunction) {
    var withAuthors = $('#includeAuthors').is(":checked");
    var withPublisher = $('#includePublisher').is(":checked");
    console.log("Get All Titles: withAuthors: " + withAuthors + "  withPublisher: " + withPublisher);

    var data = {
        "operationName": "TitlesQuery",
        "query": `  query TitlesQuery ($withAuthors: Boolean!, $withPublisher: Boolean!)
                    {
                        titles
                        {
                            titleId title price pubId type pubDate notes advance royalty ytdSales
                            authors @include(if: $withAuthors)
                            {
                                 authorId lastName firstName phone address city state zip contract
                            }
                            publisher @include(if: $withPublisher)
                            {
                                pubId name city state country
                            }
                        } 
                    }`,
        "variables": {
            "withAuthors": withAuthors,
            "withPublisher": withPublisher
        }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            processDataFunction(data.data.titles);
        },
        error: function (data) {
             console.log("error!");
             console.log(data);
        },
        contentType: 'application/json'
    });
}

function GetTitleIdAndName(processDataFunction) {
    var data = {
        "operationName": "TitlesQuery",
        "query": "query TitlesQuery { titles { titleId title } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            processDataFunction(data.data.titles);
        },
        error: function (data) {
             console.log("error!");
             console.log(data);
        },
        contentType: 'application/json'
    });
}

function DisplaySingleTitle(title) {
    var titles = [title];
    DisplayAllTitles(titles);
}

function GetTitle(titleId, processDataFunction) {
    var withAuthors = $('#includeAuthors').is(":checked");
    var withPublisher = $('#includePublisher').is(":checked");
    console.log("Get All Titles: withAuthors: " + withAuthors + "  withPublisher: " + withPublisher);

    var data = {
        "operationName": "TitleQuery",
        "query": `  query TitleQuery($titleId: String!, $withPublisher: Boolean!, $withAuthors: Boolean!) 
                    {
                        title(id: $titleId)
                        {
                            titleId title price titleId type pubDate notes advance royalty ytdSales
                            authors @include(if: $withAuthors)
                            {
                                 authorId lastName firstName phone address city state zip contract
                            }
                            publisher @include(if: $withPublisher)
                            {
                                pubId name city state country
                            }
                        }
                    }`,
        "variables": {
            "titleId": titleId,
            "withAuthors": withAuthors,
            "withPublisher": withPublisher
        }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            processDataFunction(data.data.title);
        },
        error: function (data) {
             console.log("error!");
             console.log(data);
        },
        contentType: 'application/json'
    });
}


$(document).ready(function () {
    GetTitleIdAndName(BuildSelectTitles);

    $("#allTitlesButton").click(function () {
        $("#titleSelect")[0].selectedIndex = 0;
        GetAllTitles(DisplayAllTitles);
    });

    $("#titleSelect").change(function () {
        var titleId = $(this).val();
        GetTitle(titleId, DisplaySingleTitle);
        var title = $("#titleSelect option:selected").text();
    });
});