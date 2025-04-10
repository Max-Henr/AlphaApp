document.addEventListener('DOMContentLoaded', () => {

    const modalButton = document.querySelectorAll('[data-modal="true"]');
    modalButton.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'block';

            const closeOnOutsideClick = (event) => {
                if (!modal.querySelector('.modal-content').contains(event.target)) {
                    modal.style.display = 'none';
                    document.removeEventListener('click', closeOnOutsideClick);
                }
            };

            // Use setTimeout to avoid immediate closure on same click
            setTimeout(() => {
                document.addEventListener('click', closeOnOutsideClick);
            }, 0);
        })
    })
})



const selected = new Map(); // To store selected users by ID

const container = document.getElementById("memberContainer");
const list = document.getElementById("memberList");
const search = document.getElementById("memberSearch");
const hiddenInput = document.getElementById("SelectedMemberIds");

// Show list on focus
search.addEventListener("focus", () => list.style.display = "block");

// Add member on click
list.addEventListener("click", function (e) {
    const div = e.target.closest("div[data-id]");
    if (!div) return;

    const id = div.dataset.id;
    const name = div.dataset.name;
    const avatar = div.dataset.avatar;

    if (selected.has(id)) return;

    selected.set(id, { name, avatar });
    renderPills();
    list.style.display = "none";
    search.value = "";
});

function renderPills() {
    // Clear existing pills (except the search icon)
    container.querySelectorAll(".member-pill").forEach(el => el.remove());

    // Add pills
    selected.forEach((user, id) => {
        const pill = document.createElement("div");
        pill.className = "member-pill";
        pill.innerHTML = `
        <img src="${user.avatar}" />
        ${user.name}
        <button class="remove" data-id="${id}">&times;</button>
      `;
        container.insertBefore(pill, container.querySelector(".search-icon"));
    });

    // Update hidden input
    hiddenInput.value = Array.from(selected.keys()).join(",");
}

// Remove member
container.addEventListener("click", function (e) {
    if (e.target.classList.contains("remove")) {
        const id = e.target.dataset.id;
        selected.delete(id);
        renderPills();
    }
});