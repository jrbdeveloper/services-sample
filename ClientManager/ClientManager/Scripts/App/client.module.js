var ClientModule = (function () {
    return {
        Init: function () {
            this.RegisterEvents();
        },

        RegisterEvents: function () {
            $('.delete').on('click', function (e) {
                var clientId = $(e.target).attr('client');
                var addressId = $(e.target).attr('address');

                this.Delete(clientId, addressId);
            });
        },

        Delete: function (clientId, addressId) {
            $.ajax({
                url: 'Clients/delete/',
                data: { 'clientId': clientId, 'addressId': addressId },
                dataType: 'json',
                method: 'GET'
            }).done(function (data) {
                alert('done');
            });
        }
    };
})();