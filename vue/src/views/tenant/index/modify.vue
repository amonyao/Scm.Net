<template>
	<sc-dialog
		v-model="visible"
		show-fullscreen
		:title="titleMap[mode]"
		width="700px"
		@close="close"
	>
		<el-form
			ref="formRef"
			label-width="100px"
			:model="formData"
			:rules="rules"
		>
			<el-form-item label="租户名称" prop="namec">
				<el-input
					v-model="formData.namec"
					placeholder="请输入租户名称"
					:maxlength="100"
					show-word-limit
					clearable
					:style="{ width: '100%' }"
				></el-input>
			</el-form-item>
			<el-row :gutter="15">
				<el-col :span="12">
					<el-form-item label="负责人" prop="contact">
						<el-input
							v-model="formData.contact"
							placeholder="请输入负责人"
							:maxlength="50"
							show-word-limit
							clearable
							:style="{ width: '100%' }"
						></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="联系方式" prop="cellphone">
						<el-input
							v-model="formData.cellphone"
							placeholder="请输入联系方式"
							clearable
							:style="{ width: '100%' }"
						>
						</el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="超管账号" prop="names">
						<el-input
							v-model="formData.names"
							placeholder="请输入超管账号"
							:maxlength="30"
							show-word-limit
							clearable
							:style="{ width: '100%' }"
						></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="超管密码" prop="pass">
						<el-input
							v-model="formData.pass"
							placeholder="请输入超管密码"
							:maxlength="50"
							show-word-limit
							clearable
							show-password
							:style="{ width: '100%' }"
						></el-input>
					</el-form-item>
				</el-col>
			</el-row>
		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</sc-dialog>
</template>
<script>
export default {
	emits: ["complete"],
	data() {
		return {
			mode: "add",
			titleMap: {
				add: "新增",
				edit: "编辑",
			},
			isSaveing: false,
			visible: false,
			formData: {
				id: 0,
				tenantId: 0,
				namec: undefined,
				contact: undefined,
				cellphone: undefined,
				names: undefined,
				pass: undefined
			},
			rules: {
				namec: [
					{
						required: true,
						message: "请输入租户名称",
						trigger: "blur",
					},
				],
				contact: [
					{
						required: true,
						message: "请输入负责人",
						trigger: "blur",
					},
				],
				names: [
					{
						required: true,
						message: "请输入超管账号",
						trigger: "blur",
					},
				],
				pass: [
					{
						required: true,
						message: "请输入超管密码",
						trigger: "blur",
					},
				],
				cellphone: [],
			},
		};
	},
	mounted() {},
	methods: {
		async open(row) {
			if (!row) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.urunit.model.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === 0) {
						res = await this.$API.urunit.add.post(this.formData);
					} else {
						res = await this.$API.urunit.update.put(
							this.formData
						);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("操作成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = {
				id: 0,
				tenantId: 0,
				namec: undefined,
				contact: undefined,
				cellphone: undefined,
				names: undefined,
				pass: undefined
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
