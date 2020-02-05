"use strict";
(function (g) {
    var signoutBtns = document.querySelectorAll('[data-fn="signout"]');
    signoutBtns.forEach(function (item) {
        item.addEventListener('click', HandleSignOutFn);
    });
    function HandleSignOutFn() {
        var Form = document.querySelector('#signoutForm');
        if (Form != null) {
            var _form = Form;
            _form.submit();
        }
    }
})(window);
//# sourceMappingURL=Aside.js.map