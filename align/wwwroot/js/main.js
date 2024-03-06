<script>
    function updateClock() {
        var now = new Date();
        var hours = now.getHours().toString().padStart(2, '0');
        var minutes = now.getMinutes().toString().padStart(2, '0');
        var seconds = now.getSeconds().toString().padStart(2, '0');
        var dateStr = now.toLocaleDateString('tr-TR');
        var timeStr = hours + ':' + minutes + ':' + seconds;
        document.getElementById('clock').innerText = dateStr + ' ' + timeStr;
    }

    setInterval(updateClock, 1000);
</script>