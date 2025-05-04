document.addEventListener('DOMContentLoaded', () => {
    //Open Modal
    document.querySelectorAll('[data-modal="true"]:not([data-url])').forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal) modal.style.display = 'block';

            const form = modal.querySelector('form')
            if (form) {
                setupValidationForForm(form);
            }   

            const closeOnOutsideClick = (event) => {
                if (!modal.querySelector('.modal-content').contains(event.target)) {
                    modal.style.display = 'none';
                    document.removeEventListener('click', closeOnOutsideClick);
                }
            };

            setTimeout(() => {
                document.addEventListener('click', closeOnOutsideClick);
            }, 0);
        });
    });

    //Fill Edit Modal With Content Using AJAX
    document.querySelectorAll('[data-url]').forEach(button => {
        button.addEventListener("click", async function () {
            const url = this.getAttribute("data-url");
            const target = document.getElementById("modal-placeholder");

            const response = await fetch(url);
            const html = await response.text();

            target.innerHTML = html;

            const modal = target.querySelector('.add-project-modal');
            if (modal) {
                modal.style.display = 'block';

                const form = modal.querySelector('form')
                if (form) {
                    setupValidationForForm(form);
                }

                const closeOnOutsideClick = (event) => {
                    if (!modal.querySelector('.modal-content').contains(event.target)) {
                        modal.style.display = 'none';
                        document.removeEventListener('click', closeOnOutsideClick);
                    }
                };

                setTimeout(() => {
                    document.addEventListener('click', closeOnOutsideClick);
                }, 0);
            }
        });
    });

    //DropDown
    document.querySelectorAll('[data-type="dropdown"]').forEach(button => {
        button.addEventListener('click', (e) => {
            e.stopPropagation();

            const container = button.closest('.dropdown-container');
            const dropdown = container.querySelector('.dropdown');

            dropdown.style.display = 'flex';

            const closeOnOutsideClick = (event) => {
                if (!dropdown.contains(event.target) && !button.contains(event.target)) {
                    dropdown.style.display = 'none';
                    document.removeEventListener('click', closeOnOutsideClick);
                }
            };

            setTimeout(() => {
                document.addEventListener('click', closeOnOutsideClick);
            }, 0);
        });
    });
});

//Tagit hjälp av chatgpt
document.addEventListener("DOMContentLoaded", function () {
    fetch('/Header/GetHeaderData')
        .then(response => response.json())
        .then(data => {
            const fullNameEl = document.getElementById('full-name');
            if (fullNameEl) {
                fullNameEl.textContent = data.userName;
            }
        })
        .catch(err => {
            console.error("Failed to fetch full name", err);
        });
});