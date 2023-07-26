<template>
    <div class="setting" :style="{height: `${height}px`}">
      <el-form ref="form" :model="form" label-width="90px">
        <el-row type="flex" class="row-bg" justify="center">
          <el-col :span="12">
            <div class="setting-form-title">编辑器设置：</div>
          </el-col>
        </el-row>
        <el-row type="flex" class="row-bg" justify="center">
          <el-col :span="6">
            <el-form-item label="显示行号">
              <el-switch
                v-model="form.lineNumbers"
                :active-text="form.lineNumbers ? '开':'关'">
              </el-switch>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="自动换行">
              <el-switch
                v-model="form.lineWrapping"
                :active-text="form.lineWrapping ? '开':'关'">
              </el-switch>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row type="flex" class="row-bg" justify="center">
          <el-col :span="6">
            <el-form-item label="触发提示">
              <el-select v-model="form.extraKeys" placeholder="请选择">
                <el-option label="Ctrl" value="Ctrl" />
                <el-option label="Tab" value="Tab" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="激活当前行">
              <el-switch
                v-model="form.styleActiveLine"
                :active-text="form.styleActiveLine ? '开':'关'">
              </el-switch>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row type="flex" class="row-bg" justify="center">
          <el-col :span="6">
            <el-form-item label="主题">
              <el-select v-model="form.theme" placeholder="请选择">
                <el-option label="default" value="default" />
                <el-option label="solarized light" value="solarized light" />
                <el-option label="idea" value="idea" />
                <el-option label="eclipse" value="eclipse" />
                <el-option label="cobalt" value="cobalt" />
                <el-option label="ayu-dark" value="ayu-dark" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col class="theme-prview" :span="6">
            <span class="preview-text">预览：</span>
            <img :src="`../assets/theme/${form.theme}.png`" width="200px" height="120px">
          </el-col>
        </el-row>
        <el-row type="flex" class="row-bg" justify="center">
          <el-col :span="12">
            <el-button type="primary" size="small" @click="onSave">保存</el-button>
          </el-col>
        </el-row>
      </el-form>
    </div>
  </template>
  
  <script>
  import { mapGetters, mapActions } from 'vuex'
  export default {
    data() {
      return {
        height: 0,
        form: {
          lineNumbers: false,
          lineWrapping: false,
          styleActiveLine: false,
          extraKeys: '',
          theme: ''
        }
      }
    },
    computed: {
      ...mapGetters([
        'options'
      ])
    },
    created() {
      this.form = { ...this.options }
    },
    mounted() {
      this.height = window.innerHeight - 52
      window.onresize = () => {
        this.height = window.innerHeight -  52
      }
    },
    methods: {
      ...mapActions('settings', [
        'saveOptions'
      ]),
      onSave() {
        this.saveOptions(this.form)
        this.$message.success('保存成功')
      }
    }
  }
  </script>
  
  <style lang="scss" scoped>
  .setting {
    box-sizing: border-box;
    padding: 30px 20px;
    overflow-y: scroll;
    overflow-x: hidden;
    .row-bg {
      .setting-form-title {
        margin-bottom: 20px;
        font-weight: 600;
  
      }
  
      .theme-prview {
        display: flex;
        align-items: flex-start;
  
        .preview-text {
          color: #888;
          font-size: 12px;
        }
      }
    }
  }
  .setting::-webkit-scrollbar {
    width: 5px;
  }
  .setting::-webkit-scrollbar-thumb {
    border-radius: 10px;
    -webkit-box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.2);
    background: rgba(0, 0, 0, 0.2);
  }
  </style>