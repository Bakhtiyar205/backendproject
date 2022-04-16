$(document).ready(function () {
   

    $(document).on("click", ".set-default", function () {
        let productId = parseInt($(".product-id").val());
        let imageId = parseInt($(".set-default").attr("data-id"));


        $.ajax({
            // url:"/ProductController1/LoadMore?skip" + count,
            url: "/adminArea/Product/SetDefaultImage",
            data: {
                ProductId: productId,
                ImageId: imageId
            },
            type: "Post",
            success: function (res) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Image default success',
                    showConfirmButton: false,
                    timer: 1500
                }).then(function () {
                    window.location.reload();
                })
            }
        })
    })
})