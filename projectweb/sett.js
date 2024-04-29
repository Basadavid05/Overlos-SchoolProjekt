fetch('settapi.php', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
})
.then(response => {
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    return response.json();
})
.then(data => {
    console.log(data);
    document.getElementById('first-name-span').innerText = data[0].first_name;
    document.getElementById('last-name-span').innerText = data[0].last_name;
    document.getElementById('birth-date-span').innerText = data[0].birth_date;
    document.getElementById('home-address-span').innerText = data[0].home_address;
})
.catch(error => {
    console.error('There was a problem with the fetch operation:', error);
});

document.addEventListener('DOMContentLoaded', function() {
    const tabs = document.querySelectorAll('.tab-link');
    tabs.forEach(tab => {
        tab.addEventListener('click', function(event) {
            event.preventDefault();
            const targetId = this.getAttribute('href').substring(1);
            const targetTab = document.getElementById(targetId);
            const allTabs = document.querySelectorAll('.tab-content');
            allTabs.forEach(tab => {
                tab.style.display = 'none';
            });
            targetTab.style.display = 'block';
        });
    });
});

document.addEventListener('DOMContentLoaded', function(){
    const toggleField = function(fieldId, spanId) {
        const field = document.getElementById(fieldId);
        const span = document.getElementById(spanId);
        let isOpen = false;

        return function(){
            if (!isOpen) {
                span.style.display = 'none';
                field.style.display = 'inline';
                isOpen = true;
            } else {
                span.style.display = 'inline';
                field.style.display = 'none';
                isOpen = false;
            }
        };
    };

    document.getElementById('user-change').onclick = toggleField('username', 'userr');
    document.getElementById('email-change').onclick = toggleField('email', 'emaill');
});


