<template>
    <card ref="card" />
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        card: defineAsyncComponent(() => import("./docCard"))
    },
    data() {
        return {
        };
    },
    mounted() {
        this.next();
    },
    methods: {
        async next() {
            var res = await this.$API.cmsdocarticle.daily.get(this.param);
            if (!res || res.code != 200) {
                return;
            }

            var data = res.data;
            if (!data) {
                return;
            }
            this.param.datas = data.datas;
            this.$refs.article = data.article;
        },
    },
};
</script>
