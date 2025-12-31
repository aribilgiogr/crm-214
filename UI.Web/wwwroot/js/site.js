// Leads Index Action:

const pickLead = (lead_id) => {
    if (confirm("Are you sure?")) {
        fetch(`/api/leads/pick/${lead_id}`, { method: "POST", headers: { "content-type": "application/json" } })
            .then(res => res.json())
            .then(data => {
                if (!data.success) {
                    alert(data.messages.join(", "))
                } else {
                    //location.reload()
                    const row = document.getElementById('lead_' + lead_id)
                    row.classList.remove("bg-light")
                    row.classList.add("bg-success")
                    const btn = row.querySelector(`td:last-child button`)
                    if (btn) {
                        btn.remove()
                    }
                }
            })
            .catch(err => console.error(err))
    }
}

const addActivity = (type, lead_id) => {
    if (confirm(`Create a '${type}' activity! Are you sure?`)) {
        fetch(`/api/leads/addactivity/${type}/${lead_id}`, { method: "POST", headers: { "content-type": "application/json" } })
            .then(res => res.json())
            .then(data => {
                if (!data.success) {
                    showToast(data.messages.join(", "),'Error')
                } else {
                    showToast(`'${type}' activity created!`, 'Success')
                }
            })
    }
}

function showToast(message, type = 'info') {
    // Create container if not exists
    let container = document.querySelector('.toast-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        document.body.appendChild(container);
    }

    // Create toast
    const toast = document.createElement('div');
    toast.className = 'toast';
    toast.innerHTML = `
    <div class="toast-header">
      <strong class="me-auto">${type}</strong>
      <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
    </div>
    <div class="toast-body">${message}</div>
  `;

    container.appendChild(toast);

    // Show toast
    const bsToast = new bootstrap.Toast(toast);
    bsToast.show();

    // Remove after hidden
    toast.addEventListener('hidden.bs.toast', () => toast.remove());
}
