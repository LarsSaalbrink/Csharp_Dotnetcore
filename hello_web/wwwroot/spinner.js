function spin() {
    var spinner = document.getElementById('spinner');
    var angle = 0;
    setInterval(function() {  //Direct js manipulation instead of css animation to demonstrate separate js file
        angle += 1;
        spinner.style.transform = 'rotate(' + angle + 'deg)';
    }, 33);
    document.body.style.animation = "colorchange 5s infinite";
    title = document.getElementById('mytitle');
    text = document.getElementById('mytext');
    title.innerHTML = "WOOOHHOOOOO!!!";
    text.innerHTML = "&#128512";
}