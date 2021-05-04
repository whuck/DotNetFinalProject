$(function () {
    setProductData();

    function setProductData() {
        var location = window.location.pathname;
        var url = location.substring(location.lastIndexOf("/") + 1);
        var ratingAvg;

        $.getJSON({
            url: "../../api/product/" + url,
            success: function (response, textStatus, jqXhr) {
                $('#ProductName').html(response.productName);
                $('#giveMeaName').html(response.productName);
                $('#price').html(response.unitPrice);
                console.table(response);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                // log the error to the console
                console.log("The following error occured: " + textStatus, errorThrown);
                console.log(url)
            }
        });
        $.getJSON({
            url: "../../api/review/" + url,
            success: function (response, textStatus, jqXhr) {
                console.table(response);

                // response.rating
                var count = 0;
                var sum = 0;
                response.forEach(element => {
                    count++;
                    sum += element.rating;
                    console.log(element.rating)
                    console.log(sum + ", " + count)
                    writeReview(element);
                });
                ratingAvg = sum / count;

                ratingAvg = (Math.round(ratingAvg * 2) / 2).toFixed(1);
                
                   
                

                $('#rating').html("Rating: " + makeStars(ratingAvg));
            

            },
            error: function (jqXHR, textStatus, errorThrown) {
                // log the error to the console
                console.log("The following error occured: " + textStatus, errorThrown);
                console.log(url)
            }
        });
    }

    function writeReview(review) {

        var date = new Date(review.reviewDate)

        var html = "<div class='card mb-4 border-0 col-12'>"
        + "<div  class='container review'>"
        +  "<h4>" + review.customer.companyName+ "</h4>"
        +   "<p>" + makeStars(review.rating)+ "</p>"
        +   "<p>Reviewed on: " + date.toDateString("dd/mmm/yyyy")+ "</p>"
        +   "<p>" + review.description+ "</p>"
        + "</div>"
        + "</div>"
        $('#putReviewsHere').append(html)        
    }
    
    function makeStars(rating) {
        var fullStar = "<span class='fa fa-star checked'></span>";
        var halfStar = "<span class='fa fa-star-half checked'></span>";
        var displayRating = "";

        for (let index = 0; index < rating; index++) {
            if (Math.floor(rating) > index) {
                displayRating += fullStar;

            } else if (rating > index && rating % 1 > 0) {
                displayRating += halfStar;
                

            }
        }
        return displayRating;
    }

});