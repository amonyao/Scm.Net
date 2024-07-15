<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="900px" @close="close">
		<el-container>
			<el-aside width="240px" class="no-right-border">
				<div class="select-img">
					<div class="bg-gray">
						<div class="up-wall">
							<sc-upload v-model="formData.avatar" :apiObj="uploadApi" :width="148" :height="148"
								:onSuccess="upSuccess"></sc-upload>
						</div>
					</div>
				</div>
				<div class="user-else-info">
					<p class="last-login">
						上次登录：{{ this.$TOOL.dateTimeFormat(formData.last_time) }}
					</p>
					<el-row>
						<el-col :span="8">
							<span>{{ formData.login_count }}</span>
							<p>次数</p>
						</el-col>
						<el-col :span="8">
							<span><sc-icon name="sc-check-cirrcle-fill" /></span>
							<p>状态</p>
						</el-col>
						<el-col :span="8">
							<span>13</span>
							<p>消息</p>
						</el-col>
					</el-row>
				</div>
			</el-aside>
			<el-container><el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
					<el-row>
						<el-col :span="12">
							<el-form-item label="所属机构" prop="unit_id">
								<sc-select v-model="formData.unit_id" placeholder="请选择所属机构" :change="changeUnit()"
									:data="unitList">
								</sc-select>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="登录账号" prop="codec">
								<el-input v-model="formData.codec" placeholder="请输入登录账号" :maxlength="32" show-word-limit
									clearable></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="展示姓名" prop="namec">
								<el-input v-model="formData.namec" placeholder="请输入展示姓名" :maxlength="32" show-word-limit
									clearable></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="员工简称" prop="names">
								<el-input v-model="formData.names" placeholder="请输入员工简称" :maxlength="32" show-word-limit
									clearable></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="性别" prop="sex">
								<sc-select v-model="formData.sex" :data="sex_list" />
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="登录密码" prop="pass">
								<el-input v-model="formData.pass" placeholder="请输入登录密码" :maxlength="32"
									:disabled="formData.id != 0" show-word-limit clearable show-password></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="所属角色" prop="role_list">
								<el-tree-select v-model="formData.role_list" placeholder="请选择所属角色" :data="roleList" multiple
									collapse-tags check-strictly />
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="手机" prop="cellphone">
								<el-input v-model="formData.cellphone" placeholder="请输入手机" :maxlength="11"
									clearable></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="固话" prop="telephone">
								<el-input v-model="formData.telephone" placeholder="请输入固话" :maxlength="11"
									clearable></el-input>
							</el-form-item>
						</el-col>
						<el-col :span="12">
							<el-form-item label="邮箱" prop="email">
								<el-input v-model="formData.email" placeholder="请输入邮箱" :maxlength="50" show-word-limit
									clearable></el-input>
							</el-form-item>
						</el-col>
					</el-row>

					<el-row>
						<el-col>
							<el-form-item label="描述" prop="remark">
								<el-input v-model="formData.remark" type="textarea" placeholder="请输入描述" :maxlength="200"
									show-word-limit :autosize="{ minRows: 3, maxRows: 3 }"></el-input>
							</el-form-item>
						</el-col>
					</el-row>
				</el-form>
			</el-container>
		</el-container>

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
	emits: ['complete'],
	data() {
		return {
			uploadApi: this.$API.sysfile.avatar,
			mode: "add",
			titleMap: { add: "新增", edit: "编辑", },
			isSaveing: false,
			visible: false,
			formData: this.def_data(),
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "请输入员工编码", },
				],
				namec: [
					{ required: true, trigger: "blur", message: "请输入展示姓名", },
				],
				pass: [
					{ required: true, trigger: "blur", message: "请输入登录密码", },
				],
				cellphone: [
					{ required: true, trigger: "blur", message: "请输入手机号码", },
				],
				sex: [
					{ required: true, trigger: "change", type: 'number', min: 1, message: "性别不能为空", },
				],
				unit_id: [
					{ required: true, trigger: "change", pattern: this.$SCM.REGEX_ID, message: "请选择所属机构", },
				],
				role_list: [
					{ required: true, trigger: "change", message: "请择选所属角色", },
				],
				email: [],
				remark: [],
			},
			unitList: [this.$SCM.OPTION_ONE],
			roleList: [this.$SCM.OPTION_ONE],
			sex_list: [this.$SCM.OPTION_ONE_INT],
			roleProps: { multiple: true, expandTrigger: "hover" },
			isUpHeadpic: false,
			headPicfileList: [],
		};
	},
	mounted() {
		this.$SCM.list_sex(this.sex_list, false);
	},
	methods: {
		def_data() {
			return {
				id: '0',
				unit_id: '0',
				names: "",
				namec: "",
				pass: "",
				cellphone: "",
				telephone: "",
				sex: 0,
				email: "",
				remark: "",
				avatar: undefined,
				role_list: ['0'],
				last_time: 0,
				login_count: 0,
			}
		},
		upSuccess(res) {
			this.formData.avatar = res.data.path;
			if (res.code == 200) {
				this.$message.success("上传成功~");
			} else {
				this.$message.warning(res.message);
			}
		},
		getUnit() {
			this.$SCM.list_option(this.unitList, this.$API.mgrunit.option, {}, false);
		},
		async getRole() {
			const res = await this.$API.mgrrole.option.get(this.formData.unit_id);
			if (!res || res.code != 200) {
				return;
			}

			var data = [];
			res.data.forEach(element => {
				data.push(element);
			});
			this.roleList = this.$TOOL.changeTree(data);
		},
		changeUnit() {
			if (this.formData.unit_id == '0') {
				this.roleList.length = 0;
				this.roleList.push(this.$SCM.OPTION_ONE);
				return;
			}
			this.getRole();
		},
		async open(row) {
			this.getUnit();

			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.mgruser.edit.get(row.id);
				res.data.avatar = this.$CONFIG.SERVER_URL + res.data.avatar;
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					if (this.formData.avatar) {
						this.formData.avatar = this.formData.avatar.replace(
							this.$CONFIG.SERVER_URL,
							""
						);
					}

					this.formData.pass = this.$CRYPTO.MD5(
						this.formData.pass
					);
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.mgruser.add.post(this.formData);
					} else {
						res = await this.$API.mgruser.update.put(
							this.formData
						);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("操作成功");
					} else {
						this.formData.pass = undefined;
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
<style lang="scss" scoped>
.select-img {
	border-radius: 5px;
	border: 1px solid #e6e6e6;
	padding: 10px;
	text-align: center;

	.bg-gray {
		background-color: #f5f7fa;
		border-radius: 4px;
		width: 220px;
		height: 220px;
		cursor: pointer;
		padding: 35px 0 0 35px;

		i {
			font-size: 40px;
			color: #ccd1d9;
		}
	}

	.bg-gray>.up-wall {
		width: 148px;
		height: 148px;
		overflow: hidden;
	}
}

.user-else-info {
	background-color: #f6f9fd;
	text-align: center;
	padding: 5px 0;

	.last-login {
		padding: 10px 0;
	}

	p {
		margin: 5px 0;
	}
}

.user-pic {
	width: 100%;
	height: 200px !important;
}

.cur-right {
	padding-left: 240px;
}

.phote-wall {
	width: 220px;
	height: 210px;
	position: relative;
	border: 0px;

	img {
		width: 100%;
		height: 210px;
	}

	.phote-edit {
		text-align: center;
		position: absolute;
		top: 0;
		right: 0;
		left: 0;
		bottom: 0;
		z-index: 10;
		background: rgba(0, 0, 0, 0.5);
		padding-top: 40%;
		display: none;
	}

	.el-link.el-link--default {
		color: #ffffff;
	}

	.el-link {
		font-size: 20px;
		margin: 0 10px;
	}
}

.phote-wall:hover .phote-edit {
	display: block;
}

.is-hide {
	display: none;
}

.no-right-border {
	border-right: none;
}

[data-theme="dark"] .user-else-info {
	background: #383838;
}

[data-theme="dark"] .select-img {
	border: 1px solid #6d6d6d;
}

[data-theme="dark"] .select-img .bg-gray {
	background: transparent;
}

.el-input,
el-selec {
	width: 100%;
}
</style>
