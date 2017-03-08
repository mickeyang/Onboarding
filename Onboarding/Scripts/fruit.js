function ViewModel() {
    var self = this;
    self.id = ko.observable("");
    self.name = ko.observable("");
    self.description = ko.observable("");
    self.price = ko.observable("");
    self.image = ko.observable("");

    var Fruit = {
        Id: self.id,
        Name: self.name,
        Description: self.description,
        Price: self.price,
        Image: self.image
    }

    self.Fruit = ko.observable();
    self.Fruits = ko.observableArray();

    //get fruit detail and populate data to view
    self.loadData = function () {
        $("#fruitId").ready(function () {
            var fruitId = $("#fruitId").text();
            console.log(fruitId);
            $.ajax({
                url: "/Home/FindFruit",
                type: "GET",
                data: {
                    id: fruitId
                },
                datatype: "JSON",
                success: function (result) {
                    //ViewModel = ko.mapping.fromJSON(result);
                    self.Fruit(result);
                }
            })
        })
    };  //end of detail

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
                    window.location.href = "/Home/Index";
                }
            }
        })
    };

    //edit fruit
    self.editFruit = function () {
        $.ajax({
            url: "/Home/Edit",
            type: "POST",
            data: ko.toJSON(self.Fruit),
            contentType: "application/json; charset=utf-8",
            datatype: "JSON",
            success: function (result) {
                if (result.success) {
                    $("#note").html("Fruit updated successfully");
                    window.location.href("/Home/Index");
                }
            }
        })
    };

    //remove a fruit
    self.deleteFruit = function () {
        if (confirm("Do you want to delete this fruit?")) {
            $.ajax({
                url: "/Home/Delete",
                type: "POST",
                data: ko.toJSON(self.Fruit),
                contentType: "application/json; charset=utf-8",
                datatype: "JSON",
                success: function (result) {
                    if (result.success) {
                        $("#note").html("Fruit deleted successfully");
                    }
                }
            })
        }
    };

}