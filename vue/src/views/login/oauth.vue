<template>
    12213
</template>
<script>
export default {
    name: 'oauth',
    data() {
        return {
        };
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var key = '';
            var res = await this.$API.devapp.model.get({ name: key });
            if (res == null || res.code != 200) {
                return;
            }

            this.info = res.data;
        },
        async loadTheme() {
            var res = await this.$API.login.dateTheme.get({});
            if (res == null || res.code != 200) {
                return;
            }

            this.theme = res.data;
        },
        configDark() {
            this.config.dark = this.config.dark ? false : true;
        },
        configLang(command) {
            this.config.lang = command.value;
        },
    },
}
</script>