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