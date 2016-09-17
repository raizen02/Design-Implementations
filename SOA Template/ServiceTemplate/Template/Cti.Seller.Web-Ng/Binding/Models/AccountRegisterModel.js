
(function (cr) {
    var AccountRegisterModelStep1 = function () {

        var self = this;

        self.FirstName = '';
        self.LastName = '';
        self.Address = '';
        self.City = '';
        self.State = '';
        self.ZipCode = '';

        self.Initialized = false;
    }
    cr.AccountRegisterModelStep1 = AccountRegisterModelStep1;
}(window.Seller));

(function (cr) {
    var AccountRegisterModelStep2 = function () {

        var self = this;

        self.LoginEmail = '';
        self.Password = '';
        self.PasswordConfirm = '';

        self.Initialized = false;
    }
    cr.AccountRegisterModelStep2 = AccountRegisterModelStep2;
}(window.Seller));

(function (cr) {
    var AccountRegisterModelStep3 = function () {

        var self = this;

        self.CreditCard = '';
        self.ExpDate = '';

        self.Initialized = false;
    }
    cr.AccountRegisterModelStep3 = AccountRegisterModelStep3;
}(window.Seller));
