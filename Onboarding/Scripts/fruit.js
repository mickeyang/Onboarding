function FruitView(id, name, price, description, image) {
    var self = this;
    self.id = ko.observable(id);
    self.name = ko.observable(name);
    self.description = ko.observable(description);
    self.price = ko.observable(price);
    self.image = ko.observable(image);
}