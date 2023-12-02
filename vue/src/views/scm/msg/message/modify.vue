<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-row>
				<el-col :span="24">
					<el-form-item label="接收人" prop="user_id">
						<sc-select v-model="formData.user_id" placeholder="请选择" :data="users" />
					</el-form-item>
				</el-col>
				<el-col :span="24">
					<el-form-item label="消息标题" prop="title">
						<el-input v-model="formData.title" placeholder="请输入员工工号" :maxlength="32" show-word-limit clearable
							:style="{ width: '100%' }"></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="24">
					<el-form-item label="消息内容" prop="remark">
						<el-input v-model="formData.remark" placeholder="请输入展示姓名" type="textarea"
							:style="{ width: '100%' }"></el-input>
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
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			isSaveing: false,
			visible: false,
			formData: {
				id: '0',
			},
			users: [],
			rules: {},
		};
	},
	mounted() {
		this.init();
	},
	methods: {
		async init() {
			var res = await this.$API.uruser.list.get();
			if (res == null || res.code != 200) {
				return;
			}

			this.users = res.data;
		},
		async open(row) {
			if (!row) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.sysmessage.model.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.sysmessage.add.post(this.formData);
					} else {
						res = await this.$API.sysmessage.update.put(this.formData);
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
				id: '0',
				number: undefined,
				name: undefined,
				sort: 1,
				status: true,
				summary: undefined,
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
<style scoped>
.erceipt {
	padding: 10px 15px 0px 15px;
	display: flex;
	align-content: center;
}

.erceipt .erceipt-list {
	flex: 1;
	display: flex;
	align-content: center;
	border-bottom: 1px solid #e6e7e8;
}

.erceipt .erceipt-list .item {
	line-height: 28px;
	height: 28px;
	padding: 0 10px;
	background: #f2f6fc;
	border-radius: 5px;
	margin-left: 10px;
}

.erceipt .erceipt-list .item .el-icon {
	margin-left: 10px;
	position: relative;
	top: 2px;
	cursor: pointer;
}
</style>