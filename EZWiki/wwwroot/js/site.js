// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    let reformatTimeStamps = function () {
        let timestamps = document.querySelectorAll(".timestamp");
        for (let ts of timestamps) {
            let currentTimeStamp = ts.dataset.value;
            let currentDate = new Date(currentTimeStamp);
            ts.textContent = currentDate.toString();
        
    }

    reformatTimeStamps();
})();