<template>
    <div id="oidc"></div>
</template>
<script>
export default {
    mounted() {
        window.oidc.init('oidc', { state: 'login', view: 'card', columns: 4, showCard: false });
    }
}
</script>