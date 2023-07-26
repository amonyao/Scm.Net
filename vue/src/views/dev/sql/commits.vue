<template>
    <div class="commits" :style="{height: `${height}px`}">
      <el-timeline v-if="commitsList.length > 0">
        <el-timeline-item v-for="(item,index) in commitsList" :key="item.createTime" type="success" placement="top">
          <template>
            <div class="lang-time">
              {{ item.createTime || langtimeFilter }}
            </div>
          </template>
          <el-card>
            <h4>{{ item.title }}</h4>
            <p>提交于 {{ item.createTime || timeFilter }}</p>
            <el-button type="text" @click="showDetail(item,index)">code differ</el-button>
          </el-card>
        </el-timeline-item>
      </el-timeline>
      <el-empty v-else description="暂无提交数据"></el-empty>
      <!-- 代码对比弹出框 -->
      <el-dialog title="收货地址" :visible="dialogVisible" width="90%">
        <section class="differ-tip" >
          <span>【注】左侧为历史版本，右侧为当前版本</span>
          <span class="close-differ-btn" @click="onCloseDiffer">×</span>
        </section>
        <div class="code-differ" :style="{height: `${height * 0.76}px`, width: '100%'}">
          <div id="code-editor"></div>
        </div>
      </el-dialog>
    </div>
  </template>
  
  <script>
  import CodeMirror from 'codemirror'
  import 'codemirror/lib/codemirror.css'
  import 'codemirror/addon/merge/merge.js'
  import 'codemirror/addon/merge/merge.css'
  import 'codemirror/theme/cobalt.css'
  import 'codemirror/theme/eclipse.css'
  import 'codemirror/theme/ayu-dark.css'
  import 'codemirror/theme/idea.css'
  import 'codemirror/theme/solarized.css'
  import 'codemirror/mode/sql/sql'
  import DiffMatchPatch from 'diff-match-patch'
  window.diff_match_patch = DiffMatchPatch
  window.DIFF_DELETE = -1
  window.DIFF_INSERT = 1
  window.DIFF_EQUAL = 0
  import { mapGetters } from 'vuex'
  export default {
    data() {
      return {
        height: 0,
        dialogVisible: true
      }
    },
    created() {
      this.dialogVisible = false
      // 时间降序排列
      this.commitsList.sort((a, b) => {
          return b.createTime - a.createTime
      })
    },
    mounted() {
      this.height = window.innerHeight - 55
      window.onresize = () => {
        this.height = window.innerHeight - 55
      }
    },
    computed: {
      ...mapGetters([
        'commitsList',
        'options'
      ]),
    //   commits() {
    //     return this.commitsList.sort((a, b) => {
    //       return b.createTime - a.createTime
    //     })
    //   }
    },
    filters: {
      timeFilter(t) {
        return this.$TOOL.dateFormat(t)
      },
      langtimeFilter(t) {
        //moment.locale('zh-cn')
        //return moment(t).fromNow()
        return t;
      }
    },
    methods: {
      codemirrorInit(value, orig) {
        this.$nextTick(() => {
          let target = document.getElementById('code-editor')
          target.innerHTML = ''
          const cm = CodeMirror.MergeView(target, {
            value, // 上次代码
            orig, // 本次代码
            origLeft: null,
            lineNumbers: true,
            mode: 'text/x-sparksql',
            theme: this.options.theme,
            highlightDifferences: true,
            foldGutter:true,
            lineWrapping:true,
            styleActiveLine: false,
            matchBrackets: true, 
            connect: 'align',
            revertButtons: false, // 是否显示还原按钮
            readOnly: true
          })
          // 设置左侧编辑器尺寸
          cm.editor().setSize('auto', this.height * 0.76)
          // 设置右侧编辑器尺寸
          cm.right.orig.setSize('auto', this.height * 0.76)
        })
      },
      showDetail(item, index) {
        // 查看 code differ 详情
        this.dialogVisible = true
        const orig = item.sql ? item.sql : ''
        let value = ''
        if (index < this.commitsList.length - 1) {
          const code = this.commitsList[index + 1].sql
          value = code ? code : ''
        }
        this.codemirrorInit(value, orig)
      },
      onCloseDiffer() {
        this.dialogVisible = false
      }
    }
  }
  </script>
  
  <style lang="scss" scoped>
  .commits {
    padding: 30px;
    box-sizing: border-box;
    overflow-y: scroll;
    overflow-x: hidden;
  
    .lang-time {
      line-height: 35px;
      margin-top: -18px;
    }
  }
  .commits::-webkit-scrollbar {
    width: 7px;
  }
  .commits::-webkit-scrollbar-thumb {
    border-radius: 10px;
    -webkit-box-shadow: inset 0 0 5px rgba(0, 0, 0, 0.6);
    background: rgba(0, 0, 0, 0.6);
  }
  
  .el-dialog__wrapper {
    & ::v-deep .el-dialog__header {
      display: none;
    }
    & ::v-deep .el-dialog__body {
      padding: 10px;
    }
    .code-differ {
      padding-top: 10px;
  
      #code-editor {
        height: 100%;
        overflow: hidden;
        border-bottom: 1px solid #ddd;
  
        & ::v-deep .CodeMirror-merge {
          height: 100%;
        }
  
        // & ::v-deep .CodeMirror-merge-pane {
        //   height: 100%;
        // }
      }
    }
  
    .differ-tip {
      height: 30px;
      display: flex;
      align-items: center;
      justify-content: space-between;
      box-sizing: border-box;
      padding: 0px 10px;
      .close-differ-btn {
        cursor: pointer;
        font-size: 18px;
        &:hover {
          color: #888;
        }
      }
    }
  }
  </style>