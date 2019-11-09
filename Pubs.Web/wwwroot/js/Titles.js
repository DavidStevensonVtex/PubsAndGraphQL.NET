var url = "/api/graphql";

function Startup(titles) {
    // DisplayAllTitles(titles);
    BuildSelectTitles(titles);
}

function DisplayAllTitles(titles) {
    // console.log("DisplayAllTitles");
    // console.log(titles);
    var html = "<table class='table-dark' style='width: 100%;'>";
    html += "<thead><tr><th>Title Id</th><th>Title</th><th>Price</th><th>Title Id</th><th>Type</th><th>Publication Date</th></tr></thead>";
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
        html += "<td>" + title.titleId + "</td>";
        html += "<td>" + title.type + "</td>";
        html += "<td>" + title.pubDate + "</td>";
        html += "</tr>";
    });
    html += "</tbody>";
    html += "</table>";
    $("#titles").html(html);
}

function BuildSelectTitles(titles) {
    // console.log("BuildSelectTitles");
    // console.log(titles);
    var html = "<option selected disabled>Please select an title</option>";
    titles.forEach(function (title, index) {
        html += "<option value='" + title.titleId + "'>" + title.title + "</option>";
    });
    $("#titleSelect").html(html);
}

function GetAllTitles(processDataFunction) {
    var data = {
        "operationName": "TitlesQuery",
        "query": "query TitlesQuery { titles { titleId title price titleId type pubDate notes advance royalty ytdSales } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // console.log("success!");
            processDataFunction(data.data.titles);
        },
        error: function (data) {
            // console.log("error!");
            // console.log(data);
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
            // console.log("success!");
            processDataFunction(data.data.titles);
        },
        error: function (data) {
            // console.log("error!");
            // console.log(data);
        },
        contentType: 'application/json'
    });
}

function DisplaySingleTitle(title) {
    var titles = [title];
    DisplayAllTitles(titles);
}

function GetTitle(titleId, processDataFunction) {
    // console.log("GetTitle: " + titleId);
    var variables = { "titleId": titleId };
    var data = {
        "operationName": "TitleQuery",
        "query": "query TitleQuery($titleId: String!) { title(id: $titleId)  { titleId title price titleId type pubDate notes advance royalty ytdSales } }",
        "variables": { "titleId": titleId }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // console.log("GetTitle: success!");
            processDataFunction(data.data.title);
        },
        error: function (data) {
            // console.log("error!");
            // console.log(data);
        },
        contentType: 'application/json'
    });
}


$(document).ready(function () {
    // console.log("ready!");

    GetAllTitles(Startup);
    // GetAllTitles(DisplayAllTitles);
    GetTitleIdAndName(BuildSelectTitles);

    $("#allTitlesButton").click(function () {
        // console.log("allTitlesButton clicked");
        $("#titleSelect")[0].selectedIndex = 0;
        GetAllTitles(DisplayAllTitles);
    });

    $("#titleSelect").change(function () {
        // console.log("Title Selected");
        var titleId = $(this).val();
        GetTitle(titleId, DisplaySingleTitle);
        var title = $("#titleSelect option:selected").text();
        // console.log("Selected Title: " + title + "  Selected Title Id: " + titleId);
    });
});