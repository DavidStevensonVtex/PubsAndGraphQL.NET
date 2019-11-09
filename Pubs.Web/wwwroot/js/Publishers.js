var url = "/api/graphql";

function Startup(publishers) {
    // DisplayAllPublishers(publishers);
    BuildSelectPublishers(publishers);
}

function DisplayAllPublishers(publishers) {
    // console.log("DisplayAllPublishers");
    // console.log(publishers);
    var html = "<table class='table-dark' style='width: 100%'>";
    html += "<thead><tr><th>Publisher Id</th><th>Name</th><th>City</th><th>State</th><th>Country</th></tr></thead>";
    html += "<tbody>";
    publishers.forEach(function (publisher, index) {
        html += "<tr>";
        html += "<td>" + publisher.pubId + "</td>";
        html += "<td>" + publisher.name +  "</td>";
        html += "<td>" + publisher.city + "</td>";
        html += "<td>" + publisher.state + "</td>";
        html += "<td>" + publisher.country + "</td>";
        html += "</tr>";
    });
    html += "</tbody>";
    html += "</table>";
    $("#publishers").html(html);
}

function BuildSelectPublishers(publishers) {
    // console.log("BuildSelectPublishers");
    // console.log(publishers);
    var html = "<option selected disabled>Please select an publisher</option>";
    publishers.forEach(function (publisher, index) {
        html += "<option value='" + publisher.pubId + "'>" + publisher.name + "</option>";
    });
    $("#publisherSelect").html(html);
}

function GetAllPublishers(processDataFunction) {
    var data = {
        "operationName": "PublishersQuery",
        "query": "query PublishersQuery { publishers { pubId name city state country } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // // console.log("success!");
            processDataFunction(data.data.publishers);
        },
        error: function (data) {
            // console.log("error!");
            // console.log(data);
        },
        contentType: 'application/json'
    });
}

function GetPublisherIdAndName(processDataFunction) {
    var data = {
        "operationName": "PublishersQuery",
        "query": "query PublishersQuery { publishers { pubId name } }"
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // console.log("success!");
            processDataFunction(data.data.publishers);
        },
        error: function (data) {
            // console.log("error!");
            // console.log(data);
        },
        contentType: 'application/json'
    });
}

function DisplaySinglePublisher(publisher) {
    var publishers = [publisher];
    DisplayAllPublishers(publishers);
}

function GetPublisher(pubId, processDataFunction) {
    // console.log("GetPublisher: " + pubId);
    var variables = { "pubId": pubId };
    var data = {
        "operationName": "PublisherQuery",
        "query": "query PublisherQuery($pubId: String!) { publisher(id: $pubId)  { pubId name city state country } }",
        "variables": { "pubId": pubId }
    };

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: JSON.stringify(data),
        success: function (data) {
            // console.log("GetPublisher: success!");
            processDataFunction(data.data.publisher);
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

    GetAllPublishers(Startup);
    // GetAllPublishers(DisplayAllPublishers);
    GetPublisherIdAndName(BuildSelectPublishers);

    $("#allPublishersButton").click(function () {
        // console.log("allPublishersButton clicked");
        $("#publisherSelect")[0].selectedIndex = 0;
        GetAllPublishers(DisplayAllPublishers);
    });

    $("#publisherSelect").change(function () {
        // console.log("Publisher Selected");
        var pubId = $(this).val();
        GetPublisher(pubId, DisplaySinglePublisher);
        var publisher = $("#publisherSelect option:selected").text();
        // console.log("Selected Publisher: " + publisher + "  Selected Publisher Id: " + pubId);
    });
});