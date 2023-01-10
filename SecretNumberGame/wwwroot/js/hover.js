const element = document.getElementById('hover');
element.addEventListener('mouseover', function handleMouseOver() {
    element.style.background = 'greenyellow';
    element.style.color = 'greenyellow';
    element.style.webkitTextFillColor = 'blue'
});
element.addEventListener('mouseout', function handleMouseOut() {
    element.style.background = 'mediumslateblue';
    element.style.color = 'mediumslateblue';
    element.style.webkitTextFillColor = 'mediumslateblue'

});