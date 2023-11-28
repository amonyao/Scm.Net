<template>
    <card ref="card" />
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        card: defineAsyncComponent(() => import("../docCard"))
    },
    data() {
        return {
            id: '0'
        };
    },
    mounted() {
        this.next();
    },
    methods: {
        async next() {
            this.id = this.$route.query.id;
            var res = await this.$API.cmsdocarticle.items.get(this.id);
            if (!res || res.code != 200) {
                return;
            }

            var data = res.data;
            if (!data) {
                return;
            }
            this.$refs.card.showArticle(res.data);
        },
    },
};
</script>
