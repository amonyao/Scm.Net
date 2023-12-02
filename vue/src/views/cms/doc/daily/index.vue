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
            param: {
                datas: '',
            },
        }
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
            var article = data.article;
            if (!article) {
                return;
            }
            this.$refs.card.showArticle(article);
        },
    }
}
</script>