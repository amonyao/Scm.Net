<template>
    <el-container>
        <el-main>
            <el-card>
                <div v-html="html" v-if="html"></div>
                <div v-else>
                    <div style="padding: 20px">
                        <span>暂时还没有来得及写，如果您有好意见或建议，请先通QQ交流群沟通。</span>
                    </div>
                    <img :src="img" class="image" />
                </div>
            </el-card>
        </el-main>
    </el-container>
</template>

<script>
import { useRoute } from 'vue-router';

export default {
    name: 'about',
    data() {
        return {
            img: this.$CONFIG.SERVER_URL + '/data/qq.jpg',
            html: ''
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var route = useRoute();
            var path = route.path.toLowerCase();
            var idx = path.indexOf('/app/');
            if (idx < 0) {
                return;
            }

            path = path.substring(idx + 1);
            var arr = path.split('/');
            if (arr.length < 3) {
                return;
            }

            var section = arr[1];
            var code = arr[2];

            var res = await this.$API.about.info.get({ 'code': code, 'section': section });
            if (!res || res.code != 200) {
                return;
            }

            this.html = res.data;
        }
    }
}
</script>

<style scoped>
.el-card {
    max-width: 800px;
    margin: 0 auto;
}

.image {
    width: 100%;
}

@media (max-width: 1000px) {
    .el-card {
        width: 100%;
    }
}
</style>
