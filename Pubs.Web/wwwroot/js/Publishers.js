var url = "/api/graphql";

//function Startup(publishers) {
//    // DisplayAllPublishers(publishers);
//    BuildSelectPublishers(publishers);
//}

function DisplayAllPublishers(publishers) {
    var html = "";
    html += "<table class='table table-condensed' style='width: 100%'>";
    html += "<thead style='background-color: darkblue; color: white;'><tr><th>Publisher Id</th><th>Name</th><th>City</th><th>State</th><th>Country</th></tr></thead>";
    html += "<tbody>";
    publishers.forEach(function (publisher, index) {
        html += "<tr>";
        html += "<td>" + publisher.pubId + "</td>";
        html += "<td>" + publisher.name +  "</td>";
        html += "<td>" + publisher.city + "</td>";
        html += "<td>" + publisher.state + "</td>";
        html += "<td>" + publisher.country + "</td>";
        html += "</tr>";

        if (publisher.titles !== undefined) {
            html += "<tr>";
            html += "<td>&nbsp;</td>";
            html += "<td colspan='4'>";

            html += "<table class='table' style='width: 100%'>";
            html += "<thead style='background-color: darkgreen; color: white;'><th>Title</th><th>Type</th><th>Price</th></thead>";
            console.log("Titles are defined");
            publisher.titles.forEach(function (title, index) {
                html += "<tr>";
                html += "<td style='width: 3.5in'>" + title.title + "</td>";
                html += "<td style='width: 1.5in'>" + title.type + "</td>";
                html += "<td style='width: 0.5in; text-align: right;'>$" + title.price + "</td>";
                html += "</tr>";
            });
            html += "</td>";
            html += "</tr>";
            html += "</table>";
            html += "</tr>";
        }
    });
    html += "</tbody>";
    html += "</table>";
    $("#publishers").html(html);
}

function BuildSelectPublishers(publishers) {
    var html = "<option selected disabled>Please select an publisher</option>";
    publishers.forEach(function (publisher, index) {
        html += "<option value='" + publisher.pubId + "'>" + publisher.name + "</option>";
    });
    $("#publisherSelect").html(html);
}

function GetAllPublishers(processDataFunction) {
    var withTitles = $('#includeTitles').is(":checked");
    console.log("GetAllPublishers: withTitles: " + withTitles);
    var data = {
        "operationName": "PublishersQuery",
        "query": `  query PublishersQuery ($withTitles: Boolean!)
                    {
                        publishers 
                        {
                            pubId name city state country  
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
            // // console.log("success!");
            processDataFunction(data.data.publishers);
        },
        error: function (data) {
             console.log("error!");
             console.log(data);
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
             console.log("success!");
            processDataFunction(data.data.publishers);
        },
        error: function (data) {
             console.log("error!");
             console.log(data);
        },
        contentType: 'application/json'
    });
}

function DisplaySinglePublisher(publisher) {
    var publishers = [publisher];
    DisplayAllPublishers(publishers);
}

function GetPublisher(pubId, processDataFunction) {
    var withTitles = $('#includeTitles').is(":checked");
    console.log("GetAllPublishers: withTitles: " + withTitles);
    var data = {
        "operationName": "PublisherQuery",
        "query": `  query PublisherQuery($pubId: String!, $withTitles: Boolean!) 
                    {
                        publisher(id: $pubId)
                        {
                            pubId name city state country
                            titles @include(if: $withTitles)
                            {
                                titleId title price pubId type pubDate notes advance royalty ytdSales 
                            }
                        }
                    }`,
        "variables": {
            "pubId": pubId,
            "withTitles": withTitles
        }
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
             console.log("error!");
             console.log(data);
        },
        contentType: 'application/json'
    });
}


$(document).ready(function () {
    // console.log("ready!");

    // GetAllPublishers(Startup);
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