//function FruitView(id, name, price, description, image) {
//    var self = this;
//    self.id = ko.observable(id);
//    self.name = ko.observable(name);
//    self.description = ko.observable(description);
//    self.price = ko.observable(price);
//    self.image = ko.observable(image);
//}

function ViewModel() {
    var self = this;
    self.id = ko.observable("");
    self.name = ko.observable("");
    self.description = ko.observable("");
    self.price = ko.observable("");
    self.image = ko.observable("");

    var Fruit = {
        Id:self.id,
        Name: self.name,
        Description: self.description,
        Price: self.price,
        Image:self.image
    }

    self.Fruit = ko.observable();
    self.Fruits = ko.observableArray();

    //get fruit detail and populate data to view
    $("#fruitId").ready(function () {
        var fruitId = $("#fruitId").text();
        $.ajax({
            url: "/Home/FindFruit",
            type: "GET",
            data: {
                id: fruitId
            },
            datatype: "JSON",
            success: function (result) {
                //console.log(result);
                //ViewModel = ko.mapping.fromJSON(result);
                self.Fruit(result);
                //ko.applyBindings(new FruitView(result.id, result.name, result.price, result.description, result.image));
            }
        })
    });  //end of detail

    //add a fruit
    self.addFruit = function () {
        $.ajax({
            url: "/Home/AddFruit",
            type: "POST",
            data: ko.toJSON(Fruit),
            contentType: "application/json; charset=utf-8",
            datatype: "JSON",
            success: function (result) {
                //console.log(result);
                //ViewModel = ko.mapping.fromJSON(result);
                if (result.success) {
                    $("#note").html("Fruit added successfully");
                } 
            }
        })
    };


}