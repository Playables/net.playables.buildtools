module Fastlane
  module Actions
    module SharedValues
      #UNITY_BUILD_CUSTOM_VALUE = :UNITY_BUILD_CUSTOM_VALUE
    end

    class UnityBuildAction < Action
      def self.run(params)
        # fastlane will take care of reading in the parameter and fetching the environment variable:
        #UI.message "Parameter API Token: #{params[:api_token]}"
       
       project_path = params[:project_path]
       build_target = params[:build_target]
       build_path = params[:build_path]
       build_version = params[:build_version]


       project_path = File.join(project_path,"") # make sure there's a trailing slash
       projectsettings_path = "#{project_path}ProjectSettings/ProjectVersion.txt"
       UI.user_error!("#{project_path} is no unity project. Couldn't find file at path '#{projectsettings_path}'") unless File.exist?(projectsettings_path)
      
       projectsettings_text = File.open(projectsettings_path, &:readline)
        unity_version = projectsettings_text.split(':')[1].gsub(/\s+/, "")
        UI.message "Unity version: #{unity_version}"

        unity_path = "/Applications/Unity/Hub/Editor/#{unity_version}/Unity.app/Contents/MacOS/Unity"
        UI.message "Unity path: #{unity_path}"
        UI.user_error!("Unity version #{unity_version} not found. Couldn't find file at path '#{unity_path}'") unless File.exist?(unity_path)
        
        UI.message "Unity build: #{build_target} -> #{build_path}"

        cmd = [unity_path,
          "-batchmode",
          "-quit",
          "-executeMethod","CommandlineBuild.Build",
          "-projectpath",project_path,
          "-buildtarget",build_target,
          "-buildpath","#{build_path}"]

        if build_version != -1
          UI.message "Build version: #{build_version}"
          cmd += ["-buildversion","#{build_version}"]
        end

        if system(*cmd)
          UI.success "Unity build finished."
        else
          UI.user_error! "Unity build failed."
        end


        # Actions.lane_context[SharedValues::UNITY_BUILD_CUSTOM_VALUE] = "my_val"

      end

      #####################################################
      # @!group Documentation
      #####################################################

      def self.description
        "Build with Unity"
      end

      def self.details
        # Optional:
        # this is your chance to provide a more detailed description of this action
        "You can use this action to build with Unity."
      end

      def self.available_options
        # Define all options your action supports. 
        
        # Below a few examples
        [
          FastlaneCore::ConfigItem.new(key: :project_path,
                                       env_name: "FL_UNITY_PROJECT_PATH", # The name of the environment variable
                                       description: "Project Path", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("project_path not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :build_target,
                                       env_name: "FL_UNITY_BUILD_TARGET", # The name of the environment variable
                                       description: "Build Target (https://docs.unity3d.com/ScriptReference/BuildTarget.html)", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("build_target not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :build_path,
                                       env_name: "FL_UNITY_BUILD_PATH", # The name of the environment variable
                                       description: "Build Path (relative to project path)", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("build_path not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :build_version,
                                       env_name: "FL_UNITY_BUILD_VERSION",
                                       description: "Build version (currently only implemented for Android)",
                                       is_string: false, # true: verifies the input is a string, false: every kind of value
                                       default_value: -1,
                                       ) # the default value if the user didn't provide one
        ]
      end

      def self.output
        # Define the shared values you are going to provide
        # Example
        [
         # ['UNITY_BUILD_CUSTOM_VALUE', 'A description of what this value contains']
        ]
      end

      def self.return_value
        # If your method provides a return value, you can describe here what it does
      end

      def self.authors
        # So no one will ever forget your contribution to fastlane :) You are awesome btw!
        ["Mario von Rickenbach, @any_user"]
      end

      def self.is_supported?(platform)
        # you can do things like
        # 
        #  true
        # 
        #  platform == :ios
        # 
        #  [:ios, :mac].include?(platform)
        # 

        true
        #platform == :ios
      end
    end
  end
end
