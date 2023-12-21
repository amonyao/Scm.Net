<template>
	<common-page title="用户注册">
		<el-steps :active="stepActive" simple finish-status="success">
			<el-step title="基础信息" />
			<el-step title="详细信息" />
			<el-step title="完成注册" />
		</el-steps>
		<el-form v-if="stepActive == 0" ref="stepForm_0" :model="formData" :rules="rules" :label-width="120">
			<el-form-item label="账号类型" prop="type">
				<el-radio-group v-model="formData.type" disabled>
					<el-radio-button label="1">企业用户</el-radio-button>
					<el-radio-button label="2">个人用户</el-radio-button>
				</el-radio-group>
				<div class="el-form-item-msg">系统目前仅支持公司账户申请</div>
			</el-form-item>
			<el-form-item label="公司简称" prop="unit" v-if="formData.type == 1">
				<el-input v-model="formData.unit" placeholder="请输入公司简称"></el-input>
				<div class="el-form-item-msg">公司简称将用于区分不同的账套主体</div>
			</el-form-item>
			<el-form-item label="登录账号" prop="user">
				<el-input v-model="formData.user" placeholder="请输入登录账号"></el-input>
				<div class="el-form-item-msg">登录账号将作为当前账户下管理员账户</div>
			</el-form-item>
			<el-form-item label="登录密码" prop="password1">
				<el-input v-model="formData.password1" type="password" show-password placeholder="请输入登录密码"></el-input>
				<sc-password-strength v-model="formData.password1"></sc-password-strength>
				<div class="el-form-item-msg">请输入包含英文、数字的8位以上密码</div>
			</el-form-item>
			<el-form-item label="确认密码" prop="password2">
				<el-input v-model="formData.password2" type="password" show-password placeholder="请再一次输入登录密码"></el-input>
			</el-form-item>
			<el-form-item label="" prop="agree">
				<el-checkbox v-model="formData.agree" label="">已阅读并同意</el-checkbox><span class="link"
					@click="showAgree = true">《平台服务协议》</span>
			</el-form-item>
		</el-form>
		<el-form v-if="stepActive == 1" ref="stepForm_1" :model="formData" :rules="rules" :label-width="120">
			<el-form-item label="公司全称" prop="unit_name">
				<el-input v-model="formData.unit_name" placeholder="请输入公司全称"></el-input>
			</el-form-item>
			<el-form-item label="真实姓名" prop="user_name">
				<el-input v-model="formData.user_name" placeholder="请输入真实姓名"></el-input>
			</el-form-item>
			<el-form-item label="电子邮件" prop="email">
				<el-input v-model="formData.email" placeholder="请输入电子邮件"></el-input>
			</el-form-item>
			<el-form-item label="开通服务" prop="open">
				<el-checkbox-group v-model="formData.open">
					<el-checkbox label="1">订单管理系统</el-checkbox>
					<el-checkbox label="2">仓储管理系统</el-checkbox>
					<el-checkbox label="3">园区管理系统</el-checkbox>
				</el-checkbox-group>
			</el-form-item>
		</el-form>
		<div v-if="stepActive == 2">
			<el-result icon="success" title="注册成功" :sub-title="signInName">
				<template #extra>
					<el-button type="primary" @click="goLogin">前去登录</el-button>
				</template>
			</el-result>
		</div>
		<el-form style="text-align: center;">
			<el-button v-if="stepActive > 0 && stepActive < 2" @click="pre()">上一步</el-button>
			<el-button v-if="stepActive < 1" type="primary" @click="next()">下一步</el-button>
			<el-button v-if="stepActive == 1" type="primary" @click="save()">提交</el-button>
		</el-form>
		<el-dialog v-model="showAgree" title="平台服务协议" :width="800" destroy-on-close>
			平台服务协议
			<template #footer>
				<el-button @click="showAgree = false">取消</el-button>
				<el-button type="primary" @click="showAgree = false; formData.agree = true;">我已阅读并同意</el-button>
			</template>
		</el-dialog>
	</common-page>
</template>

<script>
import scPasswordStrength from '@/components/scPasswordStrength';
import commonPage from './components/commonPage'

export default {
	components: {
		commonPage,
		scPasswordStrength
	},
	data() {
		return {
			stepActive: 0,
			showAgree: false,
			formData: this.def_data(),
			rules: {
				type: [
					{ required: true, message: '请选择账户类型' }
				],
				unit: [
					{ required: true, message: '请输入公司简称' }
				],
				user: [
					{ required: true, message: '请输入登录用户' }
				],
				password1: [
					{ required: true, message: '请输入登录密码' }
				],
				password2: [
					{ required: true, message: '请输入确认密码' },
					{
						validator: (rule, value, callback) => {
							if (value !== this.formData.password1) {
								callback(new Error('两次输入密码不一致'));
							} else {
								callback();
							}
						}
					}
				],
				agree: [
					{
						validator: (rule, value, callback) => {
							if (!value) {
								callback(new Error('请阅读并同意协议'));
							} else {
								callback();
							}
						}
					}
				],
				unit_name: [
					{ required: true, message: '请输入公司全称' }
				],
				user_name: [
					{ required: true, message: '请输入真实姓名' }
				],
				email: [
					{ required: true, message: '请输入电子邮件' }
				],
				open: [
					{ required: true, message: '请选择开通类别' }
				]
			},
			signInName: 'user@unit',
		}
	},
	mounted() {

	},
	methods: {
		def_data() {
			return {
				type: '1',
				unit: '',
				user: '',
				pass: '',
				password1: '',
				password2: '',
				agree: false,
				unit_name: '',
				user_name: '',
				email: '',
				open: []
			}
		},
		pre() {
			this.stepActive -= 1
		},
		next() {
			const formName = `stepForm_${this.stepActive}`
			this.$refs[formName].validate((valid) => {
				if (valid) {
					this.stepActive += 1
				} else {
					return false
				}
			})
		},
		save() {
			const formName = `stepForm_${this.stepActive}`
			this.$refs[formName].validate((valid) => {
				if (valid) {
					this.signon();
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

			this.stepActive += 1;
			this.signInName = '可以使用 ' + this.formData.user + '@' + this.formData.unit + ' 登录系统';
		},
		goLogin() {
			this.$router.push({
				path: '/login'
			})
		}
	}
}
</script>

<style scoped></style>
