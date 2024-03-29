<template>
    <common-page title="联合登录">
        <el-form ref="userForm" :model="formData" :rules="rules" :label-width="120" v-if="needOptions">
            <el-form-item label="联合登录" prop="type">
                <el-radio-group v-model="formData.type">
                    <el-radio-button label="1">注册新用户</el-radio-button>
                    <el-radio-button label="2">关联已有用户</el-radio-button>
                    <el-radio-button label="3">选择登录用户</el-radio-button>
                </el-radio-group>
                <div class="el-form-item-msg">请选择登录选项</div>
            </el-form-item>
            <el-form-item label="登录账户" prop="user" v-if="formData.type == 2">
                <el-input v-model="formData.user1" placeholder="请输入登录账户"></el-input>
                <div class="el-form-item-msg">请输入需要进行关联的登录账户，格式为：username@unitname</div>
            </el-form-item>
            <el-form-item label="登录账户" prop="user_id" v-if="formData.type == 3">
                <sc-select v-model="formData.user_id" placeholder="请选择登录账户"></sc-select>
                <div class="el-form-item-msg">系统检测到多个关联账户，请选择要登录的账户</div>
            </el-form-item>
            <el-form-item label="登录密码" prop="pass" v-if="formData.type == 2 || formData.type == 3">
                <el-input v-model="formData.pass" type="password" show-password placeholder="请输入登录密码"></el-input>
                <div class="el-form-item-msg">请输账户对应的登录密码</div>
            </el-form-item>
            <div style="text-align: center;">
                <el-button type="primary" @click="save()">提交</el-button>
            </div>
        </el-form>
        <div v-else class="loading">
            系统加载中……
        </div>
    </common-page>
</template>
<script>
import { useRoute } from "vue-router"
import commonPage from './components/commonPage'

export default {
    name: 'user_options',
    components: {
        commonPage
    },
    data() {
        return {
            loading: false,
            needOptions: false,
            formData: this.def_data(),
            rules: {
                type: [{ required: true, message: '请选择登录选项' }],
                user: [{ required: true, message: '请输入登录用户' }],
                user_id: [{ required: true, message: '请输入登录用户' }],
                pass: [{ required: true, message: '请输入登录密码' }]
            },
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        def_data() {
            return {
                type: '1',
                user: '',
                user_id: '0',
                pass: '',
            }
        },
        save() {
            this.$refs.userForm.validate((valid) => {
                if (valid) {
                    this.signon();
                }
            });
        },
        async init() {
            var route = useRoute();
            var key = route.query.key;
            var reg = /^[0-9a-zA-Z]{32}$/
            if (!reg.test(key)) {
                this.$router.replace({ path: '/login' });
                return;
            }

            this.checkAuth(key);
        },
        async checkAuth(key) {
            var data = { mode: 4, key: key,code:'1234' };
            var userRes = await this.$API.login.token.post(data);
            if (userRes.code != 200) {
                this.$message.warning(userRes.message);
                return false;
            }
            this.$TOOL.data.set("TOKEN", userRes.data.accessToken);
            this.$TOOL.data.set("USER_INFO", userRes.data.userInfo);
            this.$TOOL.data.set("USER_THEME", userRes.data.userTheme);

            //获取菜单
            var menuRes = await this.$API.login.authority.get();
            if (menuRes.code != 200) {
                this.$message.warning(menuRes.message);
                return false;
            }
            if (menuRes.data.length == 0) {
                this.$alert(
                    "当前用户无任何菜单权限，请联系系统管理员",
                    "无权限访问",
                    { type: "error", center: true }
                );
                return false;
            }
            this.$TOOL.data.set("MENU", menuRes.data);
            this.$TOOL.data.set("PERMISSIONS", []);

            this.loadCfg();

            let path = userRes.data.userInfo.unitId == 0 ? "/" : "/";//TODO:区分管理员与用户
            this.$router.replace({ path: path });
            this.$message.success("Login Success 登录成功");
        },
        async loadCfg() {
            var cfgRes = await this.$API.syscfgconfig.list.get({ 'types': 10 });
            cfgRes.data.forEach((item) => {
                if ("app_theme" == item.key) {
                    if (item.value == "true") {
                        document.documentElement.classList.add("dark")
                    } else {
                        document.documentElement.classList.remove("dark")
                    }
                    return;
                }
                if ("app_color" == item.key) {
                    //this.config.colorPrimary = item.val;
                    return;
                }
                if ("app_lang" == item.key) {
                    //this.config.lang = item.value;
                }
            });
        },
        async signon() {
            var data = {
                type: this.formData.type,
                unit: this.formData.unit,
                user: this.formData.user,
                pass: this.$TOOL.crypto.MD5(this.formData.password1),
                unit_name: this.formData.unit_name,
                user_name: this.formData.user_name,
                email: this.formData.email,
                open: []
            };
            var res = await this.$API.login.signon.post(data);
            if (res.code != 200) {
                this.$message.warning(res.message);
                return false;
            }
        },
    }
}
</script>
<style>
.loading {
    min-height: 400px;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
}
</style>