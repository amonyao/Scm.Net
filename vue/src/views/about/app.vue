<template>
    <el-container>
        <el-main>
            <el-card>
                <div v-html="html" v-if="html"></div>
                <div v-else>
                    <el-skeleton />
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

            var res = await this.$API.scmabout.info.get({ 'code': code, 'section': section });
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

@media (max-width: 1000px) {
    .el-card {
        width: 100%;
    }
}
</style>
