$(document).ready(function () {
    let searchTimeout;
    
    $('#searchInput').on('input', function () {
        clearTimeout(searchTimeout);
        const searchTerm = $(this).val().trim();
        
        searchTimeout = setTimeout(function () {
            if (searchTerm.length >= 2 || searchTerm.length === 0) {
                performSearch(searchTerm);
            }
        }, 300);
    });
    
    $('#clearSearch').on('click', function () {
        $('#searchInput').val('');
        performSearch('');
    });
    
    function performSearch(searchTerm) {
        $.ajax({
            url: '/Times/Search',
            type: 'GET',
            data: { searchTerm: searchTerm },
            success: function (data) {
                $('#timesContainer').html(data);
            },
            error: function (xhr, status, error) {
                console.error('Erro na busca:', error);
                $('#timesContainer').html('<div class="alert alert-danger">Erro ao carregar resultados.</div>');
            }
        });
    }
    
    // Enter key support
    $('#searchInput').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            const searchTerm = $(this).val().trim();
            performSearch(searchTerm);
        }
    });
});
